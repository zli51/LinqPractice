<Query Kind="Statements">
  <Connection>
    <ID>2157a196-5ff8-4b9d-b7b8-38ca275d501f</ID>
    <Server>.</Server>
    <Database>WorkSchedule</Database>
  </Connection>
</Query>

//1. Show all skills requiring a ticket and which employees have those skills. 
//   Include all the data as seen in the following image.
var tempdata = from x in Skills
				where x.RequiresTicket == true
				select new{
							Description = x.Description,
							Employee = (from y in EmployeeSkills
										where y.Skill.SkillID == x.SkillID
										orderby y.YearsOfExperience descending
										select new{
													Name = y.Employee.FirstName + ' ' + y.Employee.LastName,
													Level = y.Level,
													YearsOfExperience = y.YearsOfExperience
										}
							)
				};
tempdata.Dump();

//2. List all skills, alphabetically, showing only the description of the skill.
var result = from x in Skills
				orderby x.Description ascending
				select x.Description;
result.Dump();

//3. List all the skills for which we do not have any qualfied employees.
var results = from x in Skills
				where x.EmployeeSkills.Count() == 0
				select x.Description;
results.Dump();

//4. From the shifts scheduled for NAIT's placement contract, 
//   show the number of employees needed for each day (ordered by day-of-week). 
//   Bonus: display the name of the day of week (first day being Monday).
var tempdata = from x in Shifts
				where x.PlacementContract.Location.Name.Contains("NAIT")
				group x by x.DayOfWeek into g
				select new{
							Day = g.Key.ToString(),
							NumOfPeople = g.Sum(y =>y.NumberOfEmployees)
				};
tempdata.Dump();

//5. List all the employees with the most years of experience.
var MostExpe = (from x in EmployeeSkills
				select x.YearsOfExperience).Max();

var tempdata = from x in EmployeeSkills
			where x.YearsOfExperience == MostExpe
			select new{
						Name = x.Employee.FirstName + " " + x.Employee.LastName						
			};			
tempdata.Dump();