using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Repository;
using Techlabs.Euphoria.Kernel.Framework.Repository.EntityFramework;
using Techlabs.Euphoria.Kernel.Model;

namespace Kernel.Tests.OAuth.Test
{
    class UserTest
    {

        private IRepository<User> _userRepo = new EntityFrameworkRepository<User>();






        void createUser()
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {

                User user = new User { UserName="kannan",Password="password@123"};
                _userRepo.Add(user);

                unitOfWork.SaveChanges();

            }

            Console.WriteLine("Added");


        }


        public static void _Main()
        {


            new UserTest().createUser();

            Console.ReadKey();
        }
    }
}
