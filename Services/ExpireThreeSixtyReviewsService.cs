
using ThreeSixtyPlusAI.Data;

namespace ThreeSixtyPlusAI.Services
{
	public class ExpireThreeSixtyReviewsService : IHostedService, IDisposable
	{
		private readonly ILogger<ExpireThreeSixtyReviewsService> _logger;
		public IServiceProvider Services { get; }
		private Timer _timer = null!;

		public ExpireThreeSixtyReviewsService(ILogger<ExpireThreeSixtyReviewsService> logger, IServiceProvider services)
		{
			_logger = logger;
			Services = services;
		}

		public Task StartAsync(CancellationToken stoppingToken)
		{
			_logger.LogInformation("Expire 360 Reviews Service is running.");

			_timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromHours(6));

			return Task.CompletedTask;
		}

		private async void DoWork(object? state)
		{

			using var scope = Services.CreateScope();
			
				var context = scope.ServiceProvider.GetRequiredService<ThreeSixtyPlusAIContext>();

				var recordsToExpire = context.ThreeSixtyReviews
						.Where(x => DateTime.UtcNow >= x.CreatedDate.AddDays(7).ToUniversalTime())
						.ToList();

				var recordsToExpireCount = recordsToExpire.Count;

				_logger.LogInformation("{recordsToExpireCount} to expire", recordsToExpireCount);

				if (recordsToExpireCount > 0)
				{

					context.ThreeSixtyReviews.RemoveRange(recordsToExpire);
					
					await context.SaveChangesAsync();

					_logger.LogInformation("Records expired");

				}

			


		}

		public Task StopAsync(CancellationToken stoppingToken)
		{
			_logger.LogInformation("Expire 360 Reviews Service is stopping.");

			_timer?.Change(Timeout.Infinite, 0);

			return Task.CompletedTask;
		}

		public void Dispose()
		{
			_timer?.Dispose();
		}
	}

}

