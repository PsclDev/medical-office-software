namespace MoS.Objects {
    public class Patient {
        public int Id { get; set; }
        public string HealthInsurance { get; set; }
        public string InsuranceNumber { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Street { get; set; }
        public string Town { get; set; }
        public string Birthday { get; set; }
        public string Phone { get; set; }
        public string LastVisit { get; set; }
        public string Filtering { get; set; }

        public Patient() { }

        public Patient(int id, string healthInsurance, string insuranceNumber, string firstname, string lastname, string street, string town, string birthday, string phone, string lastVisit, string filtering = "") {
            Id = id;
            HealthInsurance = healthInsurance;
            InsuranceNumber = insuranceNumber;
            Firstname = firstname;
            Lastname = lastname;
            Street = street;
            Town = town;
            Birthday = birthday;
            Phone = phone;
            LastVisit = lastVisit;
            Filtering = filtering;
        }

        public override string ToString() {
            return $"{Firstname} {Lastname}";
        }
    }
}
