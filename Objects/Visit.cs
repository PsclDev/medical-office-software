namespace MoS.Objects {
    public class Visit {
        public int Id { get; set; }
        public int PatientID { get; set; }
        public string Date { get; set; }
        public string WrittenSickFromTo { get; set; }
        public string WrittenBy { get; set; }
        public string Result { get; set; }
        public string Medication { get; set; }

        public override string ToString() {
            return $"{Date}";
        }
    }
}
