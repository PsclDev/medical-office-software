namespace MoS.Objects {
    public class MenuItem {
        public string Icon { get; set; }
        public string Name { get; set; }

        public MenuItem(string icon, string name) {
            Icon = icon;
            Name = name;
        }
    }
}
