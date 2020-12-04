namespace Repository.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Repository.Entity.Domain;

    internal sealed class Configuration : DbMigrationsConfiguration<Repository.Data.MTOContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Repository.Data.MTOContext context)
        {
                        
            if (!context.Users.Any(a => a.Username == "admin"))
            {
                context.Users.Add(new User()
                {
                    FriendlyName = "admin",
                    Username="admin",
                    Password = "1",
                    Status = -1,
                    InsertDateTime = DateTime.Now
                });
                context.SaveChanges();
            }

            //if (!context.Processes.Any())
            //{
            //    context.Processes.Add(new Process()
            //    {
            //        Name = "سوهان کاری",
            //        Status = -1,
            //        InsertDateTime = DateTime.Now
            //    });

            //    context.Processes.Add(new Process()
            //    {
            //        Name = "پوست کاری",
            //        Status = -1,
            //        InsertDateTime = DateTime.Now
            //    });

            //    context.Processes.Add(new Process()
            //    {
            //        Name = "پرداخت کاری",
            //        Status = -1,
            //        InsertDateTime = DateTime.Now
            //    });

            //    context.Processes.Add(new Process()
            //    {
            //        Name = "شستشو",
            //        Status = -1,
            //        InsertDateTime = DateTime.Now
            //    });

            //    context.Processes.Add(new Process()
            //    {
            //        Name = "آبکاری",
            //        Status = -1,
            //        InsertDateTime = DateTime.Now
            //    });

            //    context.Processes.Add(new Process()
            //    {
            //        Name = "براش کاری",
            //        Status = -1,
            //        InsertDateTime = DateTime.Now
            //    });
            //}

            if (!context.Processes.Any(a => a.Name == "اتمام تولید"))
            {
                var cmd = "SET IDENTITY_INSERT dbo.processes ON " +
                          "INSERT INTO dbo.processes(ID, [Name], InsertDateTime, status) VALUES(99, N'اتمام تولید', getdate(), -1) " +
                          "  " +
                          " SET IDENTITY_INSERT dbo.processes off " +
                          "  ";

                context.Database.ExecuteSqlCommand(cmd);
            }

            if (!context.Processes.Any(a => a.Name == "اتمام تولید" && a.Id==99))
            {
                var oldEtemameTolidId = context.Processes.FirstOrDefault(a => a.Name == "اتمام تولید").Id;

                var cmd = "SET IDENTITY_INSERT dbo.processes ON " +
                          "INSERT INTO dbo.processes(ID, [Name], InsertDateTime, status) VALUES(99, N'اتمام تولید', getdate(), -1) " +
                          "  " +
                          " SET IDENTITY_INSERT dbo.processes off " +
                          "  ";

                context.Database.ExecuteSqlCommand(cmd);
                context.SaveChanges();

                var pcs = context.ProcessCategories.Where(a => a.ProcessId == oldEtemameTolidId);
                foreach (var pc in pcs)
                {
                    pc.ProcessId = 99;
                }

                var wls = context.WorkLines.Where(a => a.ProcessId == oldEtemameTolidId);
                foreach (var wl in wls)
                {
                    wl.ProcessId = 99;
                }

                context.Processes.Remove(context.Processes.FirstOrDefault(a => a.Name == "اتمام تولید" && a.Id != 99));
                context.SaveChanges();
            }

            if (!context.Processes.Any(a => a.Id == 999))
            {
                var cmd = "SET IDENTITY_INSERT dbo.processes ON " +
                          "INSERT INTO dbo.processes(ID, [Name], InsertDateTime, status) VALUES(999, N'اتمام موقت', getdate(), -1) " +
                          "  " +
                          " SET IDENTITY_INSERT dbo.processes off " +
                          "  ";

                context.Database.ExecuteSqlCommand(cmd);
                context.SaveChanges();
            }

            var catsWithProcess = context.ProcessCategories.Select(a => a.CategoryId).Distinct();
            foreach (var cat in catsWithProcess)
            {
                if(!context.ProcessCategories.Any(a=>a.CategoryId==cat && a.ProcessId==999))
                {
                    context.ProcessCategories.Add(new ProcessCategory
                    {
                        CategoryId = cat,
                        ProcessId = 999,
                        Order = 999,
                        ProcessTime = 0
                    });
                }
            }

            foreach (var cat in catsWithProcess)
            {
                if (!context.ProcessCategories.Any(a => a.CategoryId == cat && a.ProcessId == 1000))
                {
                    context.ProcessCategories.Add(new ProcessCategory
                    {
                        CategoryId = cat,
                        ProcessId = 1000,
                        Order = 1000,
                        ProcessTime = 0
                    });
                }
            }

            foreach (var cat in catsWithProcess)
            {
                if (!context.ProcessCategories.Any(a => a.CategoryId == cat && a.ProcessId == 1001))
                {
                    context.ProcessCategories.Add(new ProcessCategory
                    {
                        CategoryId = cat,
                        ProcessId = 1001,
                        Order = 1001,
                        ProcessTime = 0
                    });
                }
            }

            if (!context.Users.Any(a => a.Username == "inoutop"))
            {
                context.Users.Add(new User()
                {
                    FriendlyName = "inoutop",
                    Username = "inoutop",
                    Password = "a@123",
                    Status = -1,
                    InsertDateTime = DateTime.Now
                });
                context.SaveChanges();
            }

            var systematicProcesses = context.Processes.Where(a => a.Id == 99 || a.Id == 999 || a.Id == 1000 || a.Id == 1001).ToList();

            systematicProcesses.ForEach(a => a.Systematic = true);

            context.SaveChanges();

            if (!context.Processes.Any(a => a.Id == 1000))
            {
                var cmd = "SET IDENTITY_INSERT dbo.processes ON " +
                          "INSERT INTO dbo.processes(ID, [Name], InsertDateTime, status) VALUES(1000, N'دوباره کاری', getdate(), -1) " +
                          "  " +
                          " SET IDENTITY_INSERT dbo.processes off ";

                context.Database.ExecuteSqlCommand(cmd);
            }

            if (!context.Processes.Any(a => a.Id == 1001))
            {
                var cmd = "SET IDENTITY_INSERT dbo.processes ON " +
                          "INSERT INTO dbo.processes(ID, [Name], InsertDateTime, status) VALUES(1001, N'اسقاط', getdate(), -1) " +
                          "  " +
                          " SET IDENTITY_INSERT dbo.processes off ";

                context.Database.ExecuteSqlCommand(cmd);
            }

            context.SaveChanges();
                        
            
        }
    }
}
