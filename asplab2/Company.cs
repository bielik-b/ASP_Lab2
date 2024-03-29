namespace asplab2
{
    public class Company
    {
        public string Name { get; set; } = "";
        public int Employees { get; set; }

        public Company(string name, int employees)
        {
            Name = name;
            Employees = employees;
        }

        public Company()
        {
        }
    }
}

