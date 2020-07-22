using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeManagement;
using EmployeeManagementAccess;

namespace EmployeeManagement.Controllers
{
    public class EmployeeManagementController : ApiController
    {
        public IEnumerable<OfficeEmployee> Get()
        {
            using (EmployeeServiceEntities entities = new EmployeeServiceEntities())
            {
                return entities.OfficeEmployees.ToList();

            }
        }
        public HttpResponseMessage Get(int id)
        {
            using (EmployeeServiceEntities entities = new EmployeeServiceEntities())
            {
                var entity = entities.OfficeEmployees.FirstOrDefault(e => e.empid == id);

                if(entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with id = "+ id.ToString()+ "Not Found ");
                }

            }
        }

        public HttpResponseMessage Post([FromBody] OfficeEmployee officeEmployee)
        {
            try
            {
                using (EmployeeServiceEntities entities = new EmployeeServiceEntities())
                {
                    entities.OfficeEmployees.Add(officeEmployee);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, officeEmployee);
                    message.Headers.Location = new Uri(Request.RequestUri + officeEmployee.empid.ToString());
                    return message;
                }
            }catch(Exception ex)
            {
               return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (EmployeeServiceEntities entities = new EmployeeServiceEntities())
                {
                    var entity = entities.OfficeEmployees.FirstOrDefault(e => e.empid == id);

                    if (entity != null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with id = " + id.ToString() + "Not found to delete");

                    }
                    else
                    {
                        entities.OfficeEmployees.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        public void Put(int id, [FromBody]OfficeEmployee officeEmployee)
        {
            using(EmployeeServiceEntities entities = new EmployeeServiceEntities())
            {
                var entity = entities.OfficeEmployees.FirstOrDefault(e => e.empid == id);

                entity.empfirstName = officeEmployee.empfirstName;
                entity.empLastName = officeEmployee.empLastName;
                entity.empSalary = officeEmployee.empSalary;

                entities.SaveChanges();
            }
        }

    }
}
