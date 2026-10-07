namespace RestSharpPractice
{
    internal class Task3
    {
        public static List<string> GetAllUniqueLanguageCodes(List<CountryData> countryDataList)
        {
            var uniqueLanguages = new HashSet<string>();

            foreach (var country in countryDataList)
            {
                if (country.Languages != null)
                {
                    foreach (var langDict in country.Languages)
                    {
                        foreach (var langCode in langDict)
                        {
                            if (langCode.Key == "iso639_1" || langCode.Key == "iso639_2")
                                uniqueLanguages.Add(langCode.Value);
                        }
                    }
                }
            }

            return uniqueLanguages.ToList();
        }

        public static async Task<Dictionary<string, long>> PopulationCountByLanguageCode(CountriesApiService clientService, List<string> languageCodes)
        {
            var populationsDict = new Dictionary<string, long>();

            foreach (var languageCode in languageCodes)
            {
                var countryList = await clientService.GetLanguageDataAsync(languageCode) ?? throw new Exception("Request failed to get language data");

                long population = countryList.Sum(country => (long)country.Population);

                populationsDict.Add(languageCode, population);
            }

            return populationsDict;
        }
    }
}
