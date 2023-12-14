namespace api.Database
{
    public class ConnectionString
    {
               public string cs {get; set;}

        public ConnectionString(){
            string server = "	wb39lt71kvkgdmw0.cbetxkdyhwsb.us-east-1.rds.amazonaws.com";
            string database = "	iz1l8zkywf6542ta";
            string port = "3306";
            string userName = "xn3cb1lsqogcfsfh";
            string password = "x22rr93m52pq46ze";



            cs = $@"server = {server}; user ={userName}; database = {database}; port = {port}; password = {password};";
        }
    }
}