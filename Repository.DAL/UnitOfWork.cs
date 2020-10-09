using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using Repository.Data;
using Repository.Data.Enum;
using Repository.Data.Migrations;
using Repository.Entity.Domain;

namespace Repository.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MTOContext _mtoContext;
        private bool _dispoed;

        private BaseRepository<Category> _categories;
        private BaseRepository<User> _users;
        private BaseRepository<RatingGroup> _ratingGroups;
        private BaseRepository<RatingItem> _ratingItems;
        private BaseRepository<Tag> _tags;
        private BaseRepository<Link> _links;
        private BaseRepository<CategoryProp> _categoryProps;
        private BaseRepository<CategoryPropValue> _categoryPropValues;
        private BaseRepository<City> _cities;
        private BaseRepository<Area> _areas;
        private BaseRepository<Advertisement> _advertisements;
        private BaseRepository<AdvertisementPropValues> _advertisementPropValues;
        private BaseRepository<Product> _products;
        private BaseRepository<Customer> _customers;
        private BaseRepository<InputOutputDetail> _inputOutputDetails;
        private BaseRepository<InputOutput> _inputOutputs;
        private BaseRepository<Process> _processes;
        private BaseRepository<ProcessCategory> _processCategories;
        private BaseRepository<Worksheet> _workSheets;
        private BaseRepository<WorksheetDetail> _workSheetDetails;
        private BaseRepository<WorkLine> _workLines;

        public UnitOfWork(MTOContext mtoContext)
        {
            this._mtoContext = mtoContext;
        }

        public UnitOfWork()
        {
            this._mtoContext = new MTOContextFactory().GetMTOContext();
        }

        public BaseRepository<T> BaseRepository<T>() where T : BaseEntity
        {
            return new BaseRepository<T>(this._mtoContext);
        }


        public BaseRepository<WorkLine> WorkLines
        {
            get
            {
                return this._workLines ?? (this._workLines = new BaseRepository<WorkLine>(this._mtoContext));
            }
        }

        public BaseRepository<Worksheet> Worksheets
        {
            get
            {
                return this._workSheets ?? (this._workSheets = new BaseRepository<Worksheet>(this._mtoContext));
            }
        }

        public BaseRepository<WorksheetDetail> WorksheetDetails
        {
            get
            {
                return this._workSheetDetails ?? (this._workSheetDetails = new BaseRepository<WorksheetDetail>(this._mtoContext));
            }
        }

        public BaseRepository<Category> Categories
        {
            get
            {
                return this._categories ?? (this._categories = new BaseRepository<Category>(this._mtoContext));
            }
        }

        public BaseRepository<Process> Processes
        {
            get
            {
                return this._processes ?? (this._processes = new BaseRepository<Process>(this._mtoContext));
            }
        }

        public BaseRepository<ProcessCategory> ProcessCategories
        {
            get
            {
                return this._processCategories ?? (this._processCategories = new BaseRepository<ProcessCategory>(this._mtoContext));
            }
        }


        public BaseRepository<User> Users
        {
            get
            {
                return this._users ?? (this._users = new BaseRepository<User>(this._mtoContext));
            }
        }

        public BaseRepository<RatingGroup> RatingGroups
        {
            get
            {
                return this._ratingGroups ?? (this._ratingGroups = new BaseRepository<RatingGroup>(this._mtoContext));
            }
        }

        public BaseRepository<RatingItem> RatingItems
        {
            get
            {
                return this._ratingItems ?? (this._ratingItems = new BaseRepository<RatingItem>(this._mtoContext));
            }
        }


        public BaseRepository<Tag> Tags
        {
            get
            {
                return this._tags ?? (this._tags = new BaseRepository<Tag>(this._mtoContext));
            }
        }


        public BaseRepository<Link> Links
        {
            get
            {
                return this._links ?? (this._links = new BaseRepository<Link>(this._mtoContext));
            }
        }

        public BaseRepository<CategoryProp> CategoryProps
        {
            get
            {
                return this._categoryProps ?? (this._categoryProps = new BaseRepository<CategoryProp>(this._mtoContext));
            }
        }
        public BaseRepository<CategoryPropValue> CategoryPropValues
        {
            get
            {
                return this._categoryPropValues ?? (this._categoryPropValues = new BaseRepository<CategoryPropValue>(this._mtoContext));
            }
        }

        public BaseRepository<City> Cities
        {
            get
            {
                return this._cities ?? (this._cities = new BaseRepository<City>(this._mtoContext));
            }
        }

        public BaseRepository<Area> Areas
        {
            get
            {
                return this._areas ?? (this._areas = new BaseRepository<Area>(this._mtoContext));
            }
        }

        public BaseRepository<Advertisement> Advertisements
        {
            get
            {
                return this._advertisements ?? (this._advertisements = new BaseRepository<Advertisement>(this._mtoContext));
            }
        }


        public BaseRepository<AdvertisementPropValues> AdvertisementPropValues
        {
            get
            {
                return this._advertisementPropValues ?? (this._advertisementPropValues = new BaseRepository<AdvertisementPropValues>(this._mtoContext));
            }
        }

        public BaseRepository<Product> Products
        {
            get
            {
                return this._products ?? (this._products = new BaseRepository<Product>(this._mtoContext));
            }
        }

        public BaseRepository<Customer> Customers
        {
            get
            {
                return this._customers ?? (this._customers = new BaseRepository<Customer>(this._mtoContext));
            }
        }

        public BaseRepository<InputOutputDetail> InputOutputDetails
        {
            get
            {
                return this._inputOutputDetails ?? (this._inputOutputDetails = new BaseRepository<InputOutputDetail>(this._mtoContext));
            }
        }

        public BaseRepository<InputOutput> InputOutputs
        {
            get
            {
                return this._inputOutputs ?? (this._inputOutputs = new BaseRepository<InputOutput>(this._mtoContext));
            }
        }

        private string GetErrorText(DbEntityValidationException exception)
        {
            var errors = this.GetErrorMessage(exception.EntityValidationErrors);

            string errorText = "";

            foreach (var validationResult in errors)
            {
                errorText += validationResult.ErrorMessage + "\n";
            }

            return errorText;
        }

        public ActionResult SaveChanges()
        {
            ActionResult result = new ActionResult() { IsSuccess = false };
            try
            {
                this._mtoContext.SaveChanges();

                result = new ActionResult();
                result.IsSuccess = true;
                result.ResultCode = (int)EntityExceptionEnum.None;
                result.ResultMessage = "تغییرات با موفقیت ذخیره شد";
            }
            catch (DbEntityValidationException exception)
            {
                result.ResultCode = (int)EntityExceptionEnum.DbEntityValidationException;
                result.ResultMessage = GetErrorText(exception);

            }
            catch (DbUpdateConcurrencyException exception)
            {
                //this.ShowErrors(exception);
                result.ResultCode = (int)EntityExceptionEnum.DbUpdateConcurrencyException;
                result.Message = exception.Message;
                result.ResultMessage = exception.Message;
            }
            catch (DbUpdateException exception)
            {
                //this.ShowErrors(exception);
                result.ResultCode = (int)EntityExceptionEnum.DbUpdateException;
                result.Message = exception.Message;

                if (IsUniqueIndexException(exception))
                    result.ResultMessage = "داده های وارد شده تکراری هستند و قابلیت اضافه شدن به بانک اطلاعاتی را ندارند";
                else
                    result.ResultMessage = exception.Message;

            }
            catch (Exception exception)
            {
                //this.ShowErrors(exception);
                result.ResultCode = (int)EntityExceptionEnum.None;
                result.Message = exception.Message;
                result.ResultMessage = "خطای نامشخص";
            }
            return result;
        }

        public bool IsUniqueIndexException(Exception exception)
        {
            if (exception.InnerException != null && exception.InnerException.InnerException != null)
            {
                if (exception.InnerException.InnerException is SqlException sqlException)
                {
                    switch (sqlException.Number)
                    {
                        case 2627:  // Unique constraint error
                        case 547:   // Constraint check violation
                        case 2601:  // Duplicated key row error
                            return true;
                        default:
                            return false;
                    }
                }

            }

            return false;
        }

        //public void HandleUniqueIndexException(Exception exception)
        //{
        //    if (exception.InnerException != null && exception.InnerException.InnerException != null)
        //    {
        //        if (exception.InnerException.InnerException is SqlException sqlException)
        //        {
        //            switch (sqlException.Number)
        //            {
        //                case 2627:  // Unique constraint error
        //                case 547:   // Constraint check violation
        //                case 2601:  // Duplicated key row error
        //                            // Constraint violation exception
        //                            // A custom exception of yours for concurrency issues
        //                    throw new ConcurrencyException();
        //                default:
        //                    // A custom exception of yours for other DB issues
        //                    throw new DatabaseAccessException(
        //                      dbUpdateEx.Message, dbUpdateEx.InnerException);
        //            }
        //        }

        //        throw new DatabaseAccessException(dbUpdateEx.Message, dbUpdateEx.InnerException);
        //    }


        //    // If we're here then no exception has been thrown
        //    // So add another piece of code below for other exceptions not yet handled...
        //}

        private void ShowErrors(DbUpdateException exception)
        {
            string errorText = exception.Message;

            if (exception.InnerException != null)
            {
                errorText += "\n" + exception.InnerException.Message;

                if (exception.InnerException.InnerException != null)
                    errorText += "\n" + exception.InnerException.InnerException.Message;
            }

            //MessageBox.Show(errorText);
        }

        private void ShowErrors(DbUpdateConcurrencyException exception)
        {
            string errorText = exception.Message;

            if (exception.InnerException != null)
            {
                errorText += "\n" + exception.InnerException.Message;

                if (exception.InnerException.InnerException != null)
                    errorText += "\n" + exception.InnerException.InnerException.Message;
            }

            //MessageBox.Show(errorText);
        }

        private void ShowErrors(DbEntityValidationException exception)
        {
            var errors = this.GetErrorMessage(exception.EntityValidationErrors);

            string errorText = "";

            foreach (var validationResult in errors)
            {
                errorText += validationResult.ErrorMessage + "\n";
            }

            //MessageBox.Show(errorText);
        }

        private List<ValidationResult> GetErrorMessage(IEnumerable<DbEntityValidationResult> validationResults)
        {
            var validationResult = new List<ValidationResult>();

            foreach (var dbEntityValidationResult in validationResults)
            {
                return this.GetErrorMessage(dbEntityValidationResult.ValidationErrors);
            }

            return validationResult;
        }

        private List<ValidationResult> GetErrorMessage(IEnumerable<DbValidationError> validationErrors)
        {
            List<ValidationResult> errorMessages = (from validationError in validationErrors
                                                    select new ValidationResult(validationError.ErrorMessage,
                                                        new[] { validationError.PropertyName })).ToList<ValidationResult>();
            return errorMessages;
        }

        public void RejectChanges()
        {
            //this.DBContext.RejectChanges();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._dispoed)
            {
                if (disposing)
                {
                    this._mtoContext.Dispose();
                }
            }
            this._dispoed = true;
        }

        public ActionResult ExecCommand(string cmd, params object[] parameters)
        {
            ActionResult result = new ActionResult();
            try
            {
                _mtoContext.Database.ExecuteSqlCommand(cmd, parameters);
                result.IsSuccess = true;

            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }
    }

    public class ActionResult
    {
        public bool IsSuccess { get; set; }
        public int ResultCode { get; set; }
        public string Message { get; set; }
        public string ResultMessage { get; set; }
    }
}
