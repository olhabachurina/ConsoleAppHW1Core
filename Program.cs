using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using static ConsoleAppHW1Core.Program;

namespace ConsoleAppHW1Core;

 class Program
{
    static void Main()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
        var config = builder.Build();
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
        optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"))
        .LogTo(Console.WriteLine);
        // Добавление данных
        //using (var context = new ApplicationContext(optionsBuilder.Options))
        //{
        //    var train1 = new Train
        //    {

        //        TrainName = "105K",
        //        DepartureStation = "Odessa",
        //        ArrivalStation = "Kyiv",
        //        DepartureTime = new TimeSpan(21, 3, 0),
        //        ArrivalTime = new TimeSpan(06, 4, 5)
        //    };
        //    var train2 = new Train
        //    {

        //        TrainName = "036L",
        //        DepartureStation = "Odessa",
        //        ArrivalStation = "Przemysl",
        //        DepartureTime = new TimeSpan(13, 5, 8),
        //        ArrivalTime = new TimeSpan(08, 0, 0)
        //    };
        //    var train3 = new Train
        //    {

        //        TrainName = "12L",
        //        DepartureStation = "Odessa",
        //        ArrivalStation = "Lviv",
        //        DepartureTime = new TimeSpan(06, 2, 3),
        //        ArrivalTime = new TimeSpan(14, 0, 0)
        //    };

        //    context.Trains.AddRange(train1, train2, train3);
        //    context.SaveChanges();
        //}

        using (var context = new ApplicationContext(optionsBuilder.Options))
        {

            ////Получение данных
            //var trains = GetAllTrains(context);
            //foreach (var train in trains)
            //{
            //    Console.WriteLine($"TrainID: {train.TrainID}, TrainName: {train.TrainName}, " +
            //                      $"Departure: {train.DepartureStation} at {train.DepartureTime}, " +
            //                      $"Arrival: {train.ArrivalStation} at {train.ArrivalTime}");
            //}
            // Редактирование данных
            //int trainId = 1;
            //var newTrainData = new Train
            //{
            //    TrainName = "105Kk",
            //    DepartureStation = "Odessa",
            //    ArrivalStation = "Kyiv",
            //    DepartureTime = new TimeSpan(21, 3, 0),
            //    ArrivalTime = new TimeSpan(06, 4, 5)
            //};
            //UpdateTrain(context, trainId, newTrainData);

            // Удаление данных
            //int trainId = 2;
            //DeleteTrain(context, trainId);
        }
    }

    static void AddTrain(ApplicationContext context, Train train)
    {
        context.Trains.Add(train);
        context.SaveChanges();
    }

    static List<Train> GetAllTrains(ApplicationContext context)
    {
        return context.Trains.ToList();
    }

    static void UpdateTrain(ApplicationContext context, int trainId, Train newData)
    {
        var train = context.Trains.Find(trainId);
        if (train != null)
        {
            train.TrainName = newData.TrainName;
            train.DepartureStation = newData.DepartureStation;
            train.ArrivalStation = newData.ArrivalStation;
            train.DepartureTime = newData.DepartureTime;
            train.ArrivalTime = newData.ArrivalTime;

            context.SaveChanges();
        }
        else
        {
            Console.WriteLine($"Train with ID {trainId} not found.");
        }
    }


    static void DeleteTrain(ApplicationContext context, int trainId)
    {
        var train = context.Trains.Find(trainId);
        if (train != null)
        {
            context.Trains.Remove(train);
            context.SaveChanges();
        }
    }
}


    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Train> Trains { get; set; }
    }
    public class Train
    {
        public int TrainID { get; set; }
        public string TrainName { get; set; }
        public string DepartureStation { get; set; }
        public string ArrivalStation { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public TimeSpan ArrivalTime { get; set; }
    }
    
    





