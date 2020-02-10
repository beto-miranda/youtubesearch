using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace YoutubeSearch.Application.Infrastructure.BackgroundTasks
{
    public interface IBackgroundTaskQueue
    {
        void QueueBackgroundWorkItem(Func<CancellationToken, Task> workItem);

        Task<Func<CancellationToken, Task>> DequeueAsync(CancellationToken cancellationToken);
    }

}