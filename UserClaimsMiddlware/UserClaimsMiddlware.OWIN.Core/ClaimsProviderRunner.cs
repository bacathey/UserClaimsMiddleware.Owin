using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace UserClaimsMiddlware.OWIN.Core
{
    public interface IClaimsProviderRunner
    {
        Task<List<Claim>> RunAllProviderTasksAsync(IDictionary<string, object> envCopy,
            IList<IClaimsProvider<IClaimsProviderOptions>> providers);
    }

    public class ClaimsProviderRunner : IClaimsProviderRunner
    {
        public async Task<List<Claim>> RunAllProviderTasksAsync(IDictionary<string, object> envCopy,
            IList<IClaimsProvider<IClaimsProviderOptions>> providers)
        {
            if (envCopy == null) throw new ArgumentNullException(nameof(envCopy));
            if (providers == null) throw new ArgumentNullException(nameof(providers));

            var newClaimsList = new List<Claim>();

            var providerClaimsMasterList = await RunProviderTasksAsync(envCopy, providers);

            foreach (var list in providerClaimsMasterList)
            {
                newClaimsList.AddRange(list);
            }

            return newClaimsList;
        }

        private async Task<List<List<Claim>>> RunProviderTasksAsync(IDictionary<string, object> envCopy,
            IList<IClaimsProvider<IClaimsProviderOptions>> providers)
        {
            var getClaimsTasks = providers.Select(provider => provider.GetClaimsAsync(envCopy));

            //start tasks by calling ToArray()
            var startedTasks = getClaimsTasks.ToArray();

            var providerClaimsMasterList = new List<List<Claim>>();

            try
            {
                //wait for all tasks to complete
                var tempArray = await Task.WhenAll(startedTasks);
                providerClaimsMasterList = tempArray.ToList();
            }
            catch
            {
                //throw out the base exception of the FIRST task that faulted (in processing order)
                for (var index = 0; index <= startedTasks.Length - 1; index++)
                {
                    //good result
                    if (!startedTasks[index].Status.Equals(TaskStatus.Faulted))
                    {
                        providerClaimsMasterList.Add(startedTasks[index].Result);
                        continue;
                    }

                    //ignore exceptions
                    if (providers[index].ClaimsProviderOptions.IgnoreExceptions)
                        continue;

                    var aggregateException = startedTasks[index].Exception;
                    if (aggregateException != null)
                        throw aggregateException.GetBaseException();
                }
            }

            return providerClaimsMasterList;
        }
    }
}
