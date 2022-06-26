using Solo.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solo.BLL
{
    public class SuggestService : ISuggestService
    {
        public int AddSuggest(Suggest suggest)
        {
            using (MyContext myContext = new MyContext())
            {
                myContext.Suggests.Add(suggest);
                return myContext.SaveChanges();
            }
        }
    }
}
