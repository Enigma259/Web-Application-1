namespace WebAPI.Pictures
{
    /// <summary>
    /// This is the class ContryFlags.
    /// </summary>
    public class ContryFlags
    {
        private static volatile ContryFlags _instance;
        private static object syncRoot = new object();

        /// <summary>
        /// This is the constructor for the class ContryFlags.
        /// </summary>
        private ContryFlags()
        {
            //Insert Code Here
        }

        /// <summary>
        /// This is a multi threaded singleton for the class ContryFlags.
        /// </summary>
        /// <returns>_instance</returns>
        public static ContryFlags GetInstance()
        {
            if (_instance == null)
            {
                lock (syncRoot)
                {
                    if (_instance == null)
                    {
                        _instance = new ContryFlags();
                    }
                }
            }

            return _instance;
        }

        /// <summary>
        /// This method gets a picture of a flag.
        /// </summary>
        /// <param name="country"></param>
        /// <param name="state"></param>
        public void GetFlag(string country, string state)
        {
            string flag;

            if (country.Equals("The United States of America"))
            {
                flag = GetAmerikaFlag(state);
            }

            else
            {
                flag = GetWorldFlag(country);
            }

        }

        /// <summary>
        /// returns a picture of an american flag.
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        private string GetAmerikaFlag(string state)
        {
            string result = "";

            switch (state)
            {
                case "Alabama":

                    //Insert Code Here

                    break;

                case "Alaska":

                    //Insert Code Here

                    break;

                case "Arizona":

                    //Insert Code Here

                    break;

                case "Arkansas":

                    //Insert Code Here

                    break;

                case "California":

                    //Insert Code Here

                    break;

                case "Colorado":

                    //Insert Code Here

                    break;

                case "Connecticut":

                    //Insert Code Here

                    break;

                case "Delaware":

                    //Insert Code Here

                    break;

                case "Florida":

                    //Insert Code Here

                    break;

                case "Gorgia":

                    //Insert Code Here

                    break;

                case "Hawaii":

                    //Insert Code Here

                    break;

                case "Idaho":

                    //Insert Code Here

                    break;

                case "Illinois":

                    //Insert Code Here

                    break;

                case "Indiana":

                    //Insert Code Here

                    break;

                case "Iowa":

                    //Insert Code Here

                    break;

                case "Kansas":

                    //Insert Code Here

                    break;

                case "Kentucky":

                    //Insert Code Here

                    break;

                case "Louisiana":

                    //Insert Code Here

                    break;

                case "Maine":

                    //Insert Code Here

                    break;

                case "Maryland":

                    //Insert Code Here

                    break;

                case "Massachusetts":

                    //Insert Code Here

                    break;

                case "Michigan":

                    //Insert Code Here

                    break;

                case "Minnesota":

                    //Insert Code Here

                    break;

                case "Mississippi":

                    //Insert Code Here

                    break;

                case "Missouri":

                    //Insert Code Here

                    break;

                case "Montana":

                    //Insert Code Here

                    break;

                case "Nebraska":

                    //Insert Code Here

                    break;

                case "Nevada":

                    //Insert Code Here

                    break;

                case "New Hampshire":

                    //Insert Code Here

                    break;

                case "New Jersey":

                    //Insert Code Here

                    break;

                case "New Mexico":

                    //Insert Code Here

                    break;

                case "New York":

                    //Insert Code Here

                    break;

                case "North Carolina":

                    //Insert Code Here

                    break;

                case "North Dakota":

                    //Insert Code Here

                    break;

                case "Ohio":

                    //Insert Code Here

                    break;

                case "Oklahoma":

                    //Insert Code Here

                    break;

                case "Oregon":

                    //Insert Code Here

                    break;

                case "Pennsylvenia":

                    //Insert Code Here

                    break;

                case "Rhode Island":

                    //Insert Code Here

                    break;

                case "South Carolina":

                    //Insert Code Here

                    break;

                case "South Dakota":

                    //Insert Code Here

                    break;

                case "Tennessee":

                    //Insert Code Here

                    break;

                case "Texas":

                    //Insert Code Here

                    break;

                case "Utah":

                    //Insert Code Here

                    break;

                case "Vermont":

                    //Insert Code Here

                    break;

                case "Virginia":

                    //Insert Code Here

                    break;

                case "Washington":

                    //Insert Code Here

                    break;

                case "West Virginia":

                    //Insert Code Here

                    break;

                case "Wisconsin":

                    //Insert Code Here

                    break;

                case "Wyoming":

                    //Insert Code Here

                    break;

                default:

                    //Insert Code Here

                    break;


            }

            return result;
        }

        /// <summary>
        /// This method gets the picture of the flag of a country in the world.
        /// </summary>
        /// <param name="country"></param>
        private string GetWorldFlag(string country)
        {
            string result = "";

            switch (country)
            {
                case "Afghanistan":

                    //Insert Code Here

                    break;

                case "Albania":

                    //Insert Code Here

                    break;

                case "Algeria":

                    //Insert Code Here

                    break;

                case "Andorra":

                    //Insert Code Here

                    break;

                case "Angola":

                    //Insert Code Here

                    break;

                case "Antigua":

                    //Insert Code Here

                    break;

                case "Argentina":

                    //Insert Code Here

                    break;

                case "Armenia":

                    //Insert Code Here

                    break;

                case "Australia":

                    //Insert Code Here

                    break;

                case "Austria":

                    //Insert Code Here

                    break;

                case "Azerbaijan":

                    //Insert Code Here

                    break;

                case "Bahrain":

                    //Insert Code Here

                    break;

                case "Bangladesh":

                    //Insert Code Here

                    break;

                case "Barbados":

                    //Insert Code Here

                    break;

                case "Barbuda":

                    //Insert Code Here

                    break;

                case "Belarus":

                    //Insert Code Here

                    break;

                case "Belgium":

                    //Insert Code Here

                    break;

                case "Belize":

                    //Insert Code Here

                    break;

                case "Benin":

                    //Insert Code Here

                    break;

                case "Bhutan":

                    //Insert Code Here

                    break;

                case "Bolivia":

                    //Insert Code Here

                    break;

                case "Bosnia":

                    //Insert Code Here

                    break;

                case "Botswana":

                    //Insert Code Here

                    break;

                case "Brazil":

                    //Insert Code Here

                    break;

                case "Brunei":

                    //Insert Code Here

                    break;

                case "Bulgaria":

                    //Insert Code Here

                    break;

                case "Burkina Faso":

                    //Insert Code Here

                    break;

                case "Burundi":

                    //Insert Code Here

                    break;

                case "Cambodia":

                    //Insert Code Here

                    break;

                case "Cameroon":

                    //Insert Code Here

                    break;

                case "Canada":

                    //Insert Code Here

                    break;

                case "Cape Verde":

                    //Insert Code Here

                    break;

                case "Chile":

                    //Insert Code Here

                    break;

                case "Columbia":

                    //Insert Code Here

                    break;

                case "Costa Rica":

                    //Insert Code Here

                    break;

                case "Cote d'Ivoire":

                    //Insert Code Here

                    break;

                case "Croatia":

                    //Insert Code Here

                    break;

                case "Cuba":

                    //Insert Code Here

                    break;

                case "Cyprus":

                    //Insert Code Here

                    break;

                case "Denmark":

                    //Insert Code Here

                    break;

                case "Djibouti":

                    //Insert Code Here

                    break;

                case "Dominica":

                    //Insert Code Here

                    break;

                case "East Timor":

                    //Insert Code Here

                    break;

                case "Ecuador":

                    //Insert Code Here

                    break;

                case "Egypt":

                    //Insert Code Here

                    break;

                case "El Salvador":

                    //Insert Code Here

                    break;

                case "Equatorial Guinea":

                    //Insert Code Here

                    break;

                case "Eritrea":

                    //Insert Code Here

                    break;

                case "Estonia":

                    //Insert Code Here

                    break;

                case "Ethiopia":

                    //Insert Code Here

                    break;

                case "Fiji":

                    //Insert Code Here

                    break;

                case "Finland":

                    //Insert Code Here

                    break;

                case "France":

                    //Insert Code Here

                    break;

                case "Gabon":

                    //Insert Code Here

                    break;

                case "Georgia":

                    //Insert Code Here

                    break;

                case "Germany":

                    //Insert Code Here

                    break;

                case "Ghana":

                    //Insert Code Here

                    break;

                case "Great Britain":

                    //Insert Code Here

                    break;

                case "Greece":

                    //Insert Code Here

                    break;

                case "Grenada":

                    //Insert Code Here

                    break;

                case "Guatemala":

                    //Insert Code Here

                    break;

                case "Guinea":

                    //Insert Code Here

                    break;

                case "Guinea-Bissau":

                    //Insert Code Here

                    break;

                case "Guyana":

                    //Insert Code Here

                    break;

                case "Haiti":

                    //Insert Code Here

                    break;

                case "Herzegovina":

                    //Insert Code Here

                    break;

                case "Honduras":

                    //Insert Code Here

                    break;

                case "Hungary":

                    //Insert Code Here

                    break;

                case "Iceland":

                    //Insert Code Here

                    break;

                case "India":

                    //Insert Code Here

                    break;

                case "Indonesia":

                    //Insert Code Here

                    break;

                case "Iran":

                    //Insert Code Here

                    break;

                case "Iraq":

                    //Insert Code Here

                    break;

                case "Ireland":

                    //Insert Code Here

                    break;

                case "Israel":

                    //Insert Code Here

                    break;

                case "Italy":

                    //Insert Code Here

                    break;

                case "Jamaica":

                    //Insert Code Here

                    break;

                case "Japan":

                    //Insert Code Here

                    break;

                case "Jordan":

                    //Insert Code Here

                    break;

                case "Kazakhstan":

                    //Insert Code Here

                    break;

                case "Kenya":

                    //Insert Code Here

                    break;

                case "Kiribati":

                    //Insert Code Here

                    break;

                case "Kosovo":

                    //Insert Code Here

                    break;

                case "Kuwait":

                    //Insert Code Here

                    break;

                case "Kyrgyzstan":

                    //Insert Code Here

                    break;

                case "Laos":

                    //Insert Code Here

                    break;

                case "Latvia":

                    //Insert Code Here

                    break;

                case "Lebanon":

                    //Insert Code Here

                    break;

                case "Lesotho":

                    //Insert Code Here

                    break;

                case "Liberia":

                    //Insert Code Here

                    break;

                case "Libya":

                    //Insert Code Here

                    break;

                case "Liechtenstein":

                    //Insert Code Here

                    break;

                case "Lithuania":

                    //Insert Code Here

                    break;

                case "Luxembourg":

                    //Insert Code Here

                    break;

                case "Macedonia":

                    //Insert Code Here

                    break;

                case "Madagascar":

                    //Insert Code Here

                    break;

                case "Malawi":

                    //Insert Code Here

                    break;

                case "Malaysia":

                    //Insert Code Here

                    break;

                case "Maldives":

                    //Insert Code Here

                    break;

                case "Mali":

                    //Insert Code Here

                    break;

                case "Malta":

                    //Insert Code Here

                    break;

                case "Mauritania":

                    //Insert Code Here

                    break;

                case "Mauritius":

                    //Insert Code Here

                    break;

                case "Mexico":

                    //Insert Code Here

                    break;

                case "Micronesia":

                    //Insert Code Here

                    break;

                case "Moldova":

                    //Insert Code Here

                    break;

                case "Monaco":

                    //Insert Code Here

                    break;

                case "Mongolia":

                    //Insert Code Here

                    break;

                case "Montenegro":

                    //Insert Code Here

                    break;

                case "Morocco":

                    //Insert Code Here

                    break;

                case "Mozambique":

                    //Insert Code Here

                    break;

                case "Myanmar":

                    //Insert Code Here

                    break;

                case "Namibia":

                    //Insert Code Here

                    break;

                case "Nauru":

                    //Insert Code Here

                    break;

                case "Nepal":

                    //Insert Code Here

                    break;

                case "Nevis":

                    //Insert Code Here

                    break;

                case "New Zealand":

                    //Insert Code Here

                    break;

                case "Nicaragua":

                    //Insert Code Here

                    break;

                case "Niger":

                    //Insert Code Here

                    break;

                case "Nigeria":

                    //Insert Code Here

                    break;

                case "North Korea":

                    //Insert Code Here

                    break;

                case "Norway":

                    //Insert Code Here

                    break;

                case "Oman":

                    //Insert Code Here

                    break;

                case "Pakistan":

                    //Insert Code Here

                    break;

                case "Palau":

                    //Insert Code Here

                    break;

                case "Panama":

                    //Insert Code Here

                    break;

                case "Papua New Guinea":

                    //Insert Code Here

                    break;

                case "Paraguay":

                    //Insert Code Here

                    break;

                case "Peru":

                    //Insert Code Here

                    break;

                case "Poland":

                    //Insert Code Here

                    break;

                case "Portugal":

                    //Insert Code Here

                    break;

                case "Principe":

                    //Insert Code Here

                    break;

                case "Qatar":

                    //Insert Code Here

                    break;

                case "Romania":

                    //Insert Code Here

                    break;

                case "Russia":

                    //Insert Code Here

                    break;

                case "Rwanda":

                    //Insert Code Here

                    break;

                case "Saint Kitts":

                    //Insert Code Here

                    break;

                case "Saint Lucia":

                    //Insert Code Here

                    break;

                case "Saint Vincent":

                    //Insert Code Here

                    break;

                case "Samoa":

                    //Insert Code Here

                    break;

                case "San Marino":

                    //Insert Code Here

                    break;

                case "Sao Tome":

                    //Insert Code Here

                    break;

                case "Saudi Arabia":

                    //Insert Code Here

                    break;

                case "Senegal":

                    //Insert Code Here

                    break;

                case "Serbia":

                    //Insert Code Here

                    break;

                case "Sierra Leone":

                    //Insert Code Here

                    break;

                case "Singapore":

                    //Insert Code Here

                    break;

                case "Slovakia":

                    //Insert Code Here

                    break;

                case "Slovenia":

                    //Insert Code Here

                    break;

                case "Somalia":

                    //Insert Code Here

                    break;

                case "South Africa":

                    //Insert Code Here

                    break;

                case "South Korea":

                    //Insert Code Here

                    break;

                case "Spain":

                    //Insert Code Here

                    break;

                case "Sri Lanka":

                    //Insert Code Here

                    break;

                case "Sudan":

                    //Insert Code Here

                    break;

                case "Suriname":

                    //Insert Code Here

                    break;

                case "Swaziland":

                    //Insert Code Here

                    break;

                case "Sweden":

                    //Insert Code Here

                    break;

                case "Switzerland":

                    //Insert Code Here

                    break;

                case "Syria":

                    //Insert Code Here

                    break;

                case "Taiwan":

                    //Insert Code Here

                    break;

                case "Tajikistan":

                    //Insert Code Here

                    break;

                case "Tanzania":

                    //Insert Code Here

                    break;

                case "Tchad":

                    //Insert Code Here

                    break;

                case "Thailand":

                    //Insert Code Here

                    break;

                case "The Bahamas":

                    //Insert Code Here

                    break;

                case "The Central African Republic":

                    //Insert Code Here

                    break;

                case "The Comoros":

                    //Insert Code Here

                    break;

                case "The Czech Republic":

                    //Insert Code Here

                    break;

                case "The Democratic Republic of the Congo":

                    //Insert Code Here

                    break;

                case "The Dominican Republic":

                    //Insert Code Here

                    break;

                case "The Gambia":

                    //Insert Code Here

                    break;

                case "The Grenadines":

                    //Insert Code Here

                    break;

                case "The Marshall Islands":

                    //Insert Code Here

                    break;

                case "The Netherlands":

                    //Insert Code Here

                    break;

                case "The People's Republic of China":

                    //Insert Code Here

                    break;

                case "The Philippines":

                    //Insert Code Here

                    break;

                case "The Republic of the Congo":

                    //Insert Code Here

                    break;

                case "The Seychelles":

                    //Insert Code Here

                    break;

                case "The Solomon Islands":

                    //Insert Code Here

                    break;

                case "The United Arab Emirates":

                    //Insert Code Here

                    break;

                case "The Vatican City":

                    //Insert Code Here

                    break;

                case "Tobago":

                    //Insert Code Here

                    break;

                case "Togo":

                    //Insert Code Here

                    break;

                case "Tonga":

                    //Insert Code Here

                    break;

                case "Trinidad":

                    //Insert Code Here

                    break;

                case "Tunisia":

                    //Insert Code Here

                    break;

                case "Turkey":

                    //Insert Code Here

                    break;

                case "Turkmenistan":

                    //Insert Code Here

                    break;

                case "Tuvalu":

                    //Insert Code Here

                    break;

                case "Uganda":

                    //Insert Code Here

                    break;

                case "Ukraine":

                    //Insert Code Here

                    break;

                case "Uruguay":

                    //Insert Code Here

                    break;

                case "Uzbekistan":

                    //Insert Code Here

                    break;

                case "Vanuatu":

                    //Insert Code Here

                    break;

                case "Venezuela":

                    //Insert Code Here

                    break;

                case "Vietnam":

                    //Insert Code Here

                    break;

                case "Western Sahara":

                    //Insert Code Here

                    break;

                case "Yemen":

                    //Insert Code Here

                    break;

                case "Zambia":

                    //Insert Code Here

                    break;

                case "Zimbabwe":

                    //Insert Code Here

                    break;

                default:

                    //Insert Code Here

                    break;
            }

            return result;
        }
    }
}
