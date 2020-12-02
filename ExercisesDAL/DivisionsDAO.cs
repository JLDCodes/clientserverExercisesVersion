using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection; // compiler figures out method name using meta data 
using Microsoft.EntityFrameworkCore;

namespace ExercisesDAL
{
    public class DivisionDAO
    {
        readonly IRepository<Divisions> repository;

        public DivisionDAO()
        {
           repository = new SomeSchoolRepository<Divisions>();
        }


        public List<Divisions> GetAll()
        {

            try
            {

                return repository.GetAll();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }


        }

    }
}
