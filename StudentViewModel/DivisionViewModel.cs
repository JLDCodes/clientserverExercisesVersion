using ExercisesDAL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ExercisesViewModels
{
    public class DivisionViewModel
    {
        public string Name { get; set; }
        public int Id { get; set; }

        readonly private DivisionDAO _dao;
        public DivisionViewModel()
        {
            _dao = new DivisionDAO();
        }

        public List<DivisionViewModel> GetAll()
        {
            List<DivisionViewModel> allVms = new List<DivisionViewModel>();
            try
            {
                List<Divisions> allDivisions = _dao.GetAll();
                foreach (Divisions div in allDivisions)
                {
                    DivisionViewModel divVm = new DivisionViewModel
                    {
                        Id = div.Id,
                        Name = div.Name,
                    };
                    allVms.Add(divVm);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return allVms;
        }

    }
}
