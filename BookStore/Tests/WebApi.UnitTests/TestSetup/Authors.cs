using System;
using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup
{
    public static class Authors
    {
        public static void AddAuthors(this BookStoreDBContext context)
        {
           context.Authors.AddRange(
                        new Author{
                            Name = "İsim Test 1",
                            Surname = "Soyisim Test 1",
                            Birthdate = new DateTime(2001, 06,12)
                        },
                        new Author{
                            Name = "İsim Test 2",
                            Surname = "Soyisim Test 2",
                            Birthdate = new DateTime(2002, 06,12)
                        },
                        new Author{
                            Name = "İsim Test 3",
                            Surname = "Soyisim Test 3",
                            Birthdate = new DateTime(2003, 06,12)
                        }

                    );
        }
    }
}