using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plot
{
    public class Request
    {
        public int requestId;
        public int start;            //relative time in seconds
        public int end;
        public float  duration;
        public string method;
        public string requeststring;
        public string token;
    }
    public class UserAction
    {
        public List<Request> requests;
        public string username;
        public int userId;
    }
    public class iisvm
    {
        iisdata _data;
        public int totalruntime;         //miliseconds
        public int totolusers;
        public static string[] requestmapping;
        public static string[] usermapping;
        public UserAction[] useractions;
        static iisvm()
        {
            string path = @"C:\Users\JLiang\Documents\Visual Studio 2015\Projects\ConsoleApplication1\plot\rquests.txt";
            requestmapping = System.IO.File.ReadAllLines(path);
            string path2 = @"C:\Users\JLiang\Documents\Visual Studio 2015\Projects\ConsoleApplication1\plot\users.txt";
            usermapping = System.IO.File.ReadAllLines(path2);
        }

        public iisvm(iisdata data)
        {
            this._data = data;
            this.totalruntime = data.getMax();
            this.totolusers = data.getSessionNum();
            useractions = new UserAction[totolusers];
            for (int i = 0; i < useractions.Length; i++)
            {
                useractions[i] = new UserAction();
                useractions[i].requests = new List<Request>();
            }
            var records = _data.getData();
          
            records.ForEach(r => {
                int index = r.sessionId;
                var uact = useractions[index];
                if (r.user.Length > 2)
                    uact.userId = usermapping.Select((s, i) => new { i, s })
                                                .Where(t => t.s.Contains(r.user))
                                                .Select(t => t.i)
                                                .First();
                uact.username = r.user;
                uact.requests.Add(new Request {  start = r.dx,
                                                 duration = (float) Convert.ToInt32(r.taken) / 1000,
                                                 requeststring = r.drequest,
                                                 requestId = Array.IndexOf(requestmapping,r.drequest)
                                               });
            });
        }

       

    }
}
