using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConvertCSV
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Working");

            List<nbaStat> nbaStatList = new List<nbaStat>();

            List<CollegeStats> collegeStatsList = new List<CollegeStats>();
            List<AllStats> allStatsList = new List<AllStats>();

            int PlayersNotFound = 0;

            //reading first list
            using (var reader = new System.IO.StreamReader(@"C:\Users\Arno\Documents\GitHub\NBAStats\ConvertCSV\ConvertCSV\NBAStats.csv"))
            {
                reader.ReadLine(); //ignore first line
                reader.ReadLine();


                string line = reader.ReadLine();
                while (line != null)
                {
                    string[] parts = line.Split(',');

                    nbaStat nba = new nbaStat();
                    nba.Year = int.Parse(parts[1]);
                    nba.Lg = parts[2];
                    nba.Rd = int.Parse(parts[3]);
                    nba.Pk = int.Parse(parts[4]);
                    nba.Tm = parts[5];
                    nba.Player = parts[6];
                    nba.age = parts[7];
                    nba.pos = parts[8];
                    nba.College = parts[10];

                    nbaStatList.Add(nba);

                    line = reader.ReadLine();
                }
            }//end read nba stats

            using (var reader = new System.IO.StreamReader(@"C:\Users\Arno\Documents\GitHub\NBAStats\ConvertCSV\ConvertCSV\laatsteStuks.csv"))
            {
                reader.ReadLine(); //ignore first line
                reader.ReadLine();


                string line = reader.ReadLine();
                while (line != null)
                {
                    string[] parts = line.Split(',');

                    CollegeStats c = new CollegeStats();

                    //Rk,Player,Class,Season,Pos,School,Conf,G,MP,FG,FGA,2P,2PA,3P,3PA,FT,FTA,ORB,DRB,TRB,AST,STL,BLK,TOV,PF,PTS

                    c.Player = parts[1];
                    c.Class = parts[2];
                    c.Season = parts[3];
                    c.pos = parts[4];
                    c.school = parts[5];

                    c.Games = int.Parse(parts[7]);
                    c.Mp = (parts[8] != "") ? int.Parse(parts[8]) : 0;
                    c.FG = int.Parse(parts[9]);
                    c.FGA = int.Parse(parts[10]);
                    c.twop = (parts[11] != "") ?  int.Parse(parts[11]): 0;
                    c.twoPA = (parts[12] != "") ?  int.Parse(parts[12]): 0;
                    c.treeP = (parts[13] != "") ?  int.Parse(parts[13]): 0;
                    c.treePA = (parts[14] != "")?  int.Parse(parts[14]): 0;
                    c.FT = int.Parse(parts[15]);
                    c.FTA = int.Parse(parts[16]);
                    c.ORB = (parts[17] != "") ? int.Parse(parts[17]) : 0;
                    c.DRB = (parts[18] != "") ? int.Parse(parts[18]) : 0;
                    c.TRB = int.Parse(parts[19]);
                    c.AST = int.Parse(parts[20]);
                    c.STL = int.Parse(parts[21]);
                    c.BLK = int.Parse(parts[22]);
                    c.TOV = (parts[23] != "") ? int.Parse(parts[23]) : 0;
                    c.PF = (parts[24] != "") ? int.Parse(parts[24]) : 0;
                    c.PTS = int.Parse(parts[25]);

                    collegeStatsList.Add(c);

                    line = reader.ReadLine();
                }
            }//end read coll stats


            using (var reader = new System.IO.StreamReader(@"C:\Users\Arno\Documents\GitHub\NBAStats\ConvertCSV\ConvertCSV\CollegeStatsFirst1000.csv"))
            {
                reader.ReadLine(); //ignore first line
                reader.ReadLine();


                string line = reader.ReadLine();
                while (line != null)
                {
                    string[] parts = line.Split(',');

                    CollegeStats c = new CollegeStats();

                    //Rk,Player,Class,Season,Pos,School,Conf,G,MP,FG,FGA,2P,2PA,3P,3PA,FT,FTA,ORB,DRB,TRB,AST,STL,BLK,TOV,PF,PTS

                    c.Player = parts[1];
                    c.Class = parts[2];
                    c.Season = parts[3];
                    c.pos = parts[4];
                    c.school = parts[5];

                    c.Games = int.Parse(parts[7]);
                    c.Mp = (parts[8] != "") ? int.Parse(parts[8]) : 0;
                    c.FG = int.Parse(parts[9]);
                    c.FGA = int.Parse(parts[10]);
                    c.twop = int.Parse(parts[11]);
                    c.twoPA = int.Parse(parts[12]);
                    c.treeP = int.Parse(parts[13]);
                    c.treePA = int.Parse(parts[14]);
                    c.FT = int.Parse(parts[15]);
                    c.FTA = int.Parse(parts[16]);
                    c.ORB = (parts[17] != "") ? int.Parse(parts[17]) : 0;
                    c.DRB = (parts[18] != "") ? int.Parse(parts[18]) : 0;
                    c.TRB = int.Parse(parts[19]);
                    c.AST = int.Parse(parts[20]);
                    c.STL = int.Parse(parts[21]);
                    c.BLK = int.Parse(parts[22]);
                    c.TOV = (parts[23] != "") ? int.Parse(parts[23]) : 0;
                    c.PF = (parts[24] != "") ? int.Parse(parts[24]) : 0;
                    c.PTS = int.Parse(parts[25]);

                    collegeStatsList.Add(c);

                    line = reader.ReadLine();
                }
            }//end read coll2stats

            List<int> lst = new List<int>();
            foreach (nbaStat stat in nbaStatList)
            {
                int found = 0;
                //all nba pics in stat

                CollegeStats newCstat = new CollegeStats();
                List<CollegeStats> lstNewCstat = new List<CollegeStats>();

                foreach (CollegeStats cstat in collegeStatsList)
                {
                    string[] p1 = stat.Player.Split('\\');
                    string[] p2 = cstat.Player.Split('\\');
                    if (p1[0] == p2[0])
                    {
                        found++;
                        //Console.WriteLine("found");
                        lstNewCstat.Add(cstat);
                    }
                }
                if (found == 0)
                    Console.WriteLine("Not found count: " + PlayersNotFound ++);
                if (found == 1)
                {
                    newCstat = lstNewCstat[0];
                    AllStats p = new AllStats();

                    p.Year = stat.Year;
                    p.Lg = stat.Lg;
                    p.Rd = stat.Rd;
                    p.Pk = stat.Pk;
                    p.Tm = stat.Tm;
                    p.Player = stat.Player;
                    p.age = stat.age;
                    p.pos = stat.pos;
                    p.College = stat.College;
                    p.Class = newCstat.Class;
                    p.Season = 1;
                    p.Games = newCstat.Games;
                    p.Mp = newCstat.Mp;
                    p.FG = newCstat.FG;
                    p.FGA = newCstat.FGA;
                    p.twop = newCstat.twop;
                    p.twoPA = newCstat.twoPA;
                    p.treeP = newCstat.treeP;
                    p.treePA = newCstat.treePA;
                    p.FT = newCstat.FT;
                    p.FTA = newCstat.FTA;
                    p.ORB = newCstat.ORB;
                    p.DRB = newCstat.DRB;
                    p.TRB = newCstat.TRB;
                    p.AST = newCstat.AST;
                    p.STL = newCstat.STL;
                    p.BLK = newCstat.BLK;
                    p.TOV = newCstat.TOV;
                    p.PF = newCstat.PF;
                    p.PTS = newCstat.PTS;

                    allStatsList.Add(p);
                }
                if(found > 1)
                {
                    found--;

                    AllStats p = new AllStats();

                    p.Year = stat.Year;
                    p.Lg = stat.Lg;
                    p.Rd = stat.Rd;
                    p.Pk = stat.Pk;
                    p.Tm = stat.Tm;
                    p.Player = stat.Player;
                    p.age = stat.age;
                    p.pos = stat.pos;
                    p.College = stat.College;
                    p.Class = lstNewCstat[found].Class;
                    p.Season = found + 1;

                    for(int i = 0; i <found; i++)
                    {
                        p.Games += lstNewCstat[i].Games;
                        p.Mp += lstNewCstat[i].Mp;
                        p.FG += lstNewCstat[i].FG;
                        p.FGA += lstNewCstat[i].FGA;
                        p.twop += lstNewCstat[i].twop;
                        p.twoPA += lstNewCstat[i].twoPA;
                        p.treeP += lstNewCstat[i].treeP;
                        p.treePA += lstNewCstat[i].treePA;
                        p.FT += lstNewCstat[i].FT;
                        p.FTA += lstNewCstat[i].FTA;
                        p.ORB += lstNewCstat[i].ORB;
                        p.DRB += lstNewCstat[i].DRB;
                        p.TRB += lstNewCstat[i].TRB;
                        p.AST += lstNewCstat[i].AST;
                        p.STL += lstNewCstat[i].STL;
                        p.BLK += lstNewCstat[i].BLK;
                        p.TOV += lstNewCstat[i].TOV;
                        p.PF += lstNewCstat[i].PF;
                        p.PTS += lstNewCstat[i].PTS;
                    }

                    allStatsList.Add(p);

                }

               
            }


            //write csv
            using (var writer = new StreamWriter(@"C:\Users\Arno\Documents\GitHub\NBAStats\ConvertCSV\ConvertCSV\Output.csv"))
            {
                //headers
                writer.WriteLine("Year, Round, Pick, Team, Player, Age, Position, College, Class, Season, Games, Minutes, FG, FGA, 2points," +
                    " 2points a, 3points, 3points a, FreeTrow, FreeTrow a, Offensive rebounds, Defensive rebounds," +
                    " Total rebounds, Assists, Steal, Blocks, Turnover, Fouls, Points");


                foreach(AllStats s in allStatsList)
                {
                    string line = s.Year + ","
                        + s.Rd + ","
                        + s.Pk + ","
                        + s.Tm + ","
                        + s.Player + ","
                        + s.age + ","
                        + s.pos + ","
                        + s.College + ","
                        + s.Class + ","
                        + s.Season + ","
                        + s.Games + ","
                        + s.Mp + ","
                        + s.FG + ","
                        + s.FGA + ","
                        + s.twop + ","
                        + s.twoPA + ","
                        + s.treeP + ","
                        + s.treePA + ","
                        + s.FT + ","
                        + s.FTA + ","
                        + s.ORB + ","
                        + s.DRB + ","
                        + s.TRB + ","
                        + s.AST + ","
                        + s.STL + ","
                        + s.BLK + ","
                        + s.TOV + ","
                        + s.PF + ","
                        + s.PTS;
                    writer.WriteLine(line);
                    writer.Flush();
                }
            }



            Console.ReadLine();
        }
    }


    class AllStats
    {
        public int Year;
        public string Lg;
        public int Rd;
        public int Pk;
        public String Tm;
        public string Player;
        public string age;
        public string pos;
        public string College;
        public string Class;
        public int Season;
        public int Games;
        public int Mp;
        public int FG;
        public int FGA;
        public int twop;
        public int twoPA;
        public int treeP;
        public int treePA;
        public int FT;
        public int FTA;
        public int ORB;
        public int DRB;
        public int TRB;
        public int AST;
        public int STL;
        public int BLK;
        public int TOV;
        public int PF;
        public int PTS;
    }

    class CollegeStats
    {
        public string Player;
        public string Class;

        public String Season;
        public string pos;
        public string school;
        public int Games;
        public int Mp;
        public int FG;
        public int FGA;
        public int twop;
        public int twoPA;
        public int treeP;
        public int treePA;
        public int FT;
        public int FTA;
        public int ORB;
        public int DRB;
        public int TRB;
        public int AST;
        public int STL;
        public int BLK;
        public int TOV;
        public int PF;
        public int PTS;
    }

    class nbaStat
    {
        public int Year;
        public string Lg;
        public int Rd;
        public int Pk;
        public String Tm;
        public string Player;
        public string age;
        public string pos;
        public string College;
    }
}
