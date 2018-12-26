using System.Collections.Generic;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Buildings;

namespace HuggableScarecrows
{
    public class ModEntry : Mod
    {
        public override void Entry(IModHelper helper)
        {
            helper.Events.GameLoop.DayStarted += GameLoop_DayStarted;
        }

        private void GameLoop_DayStarted(object sender, StardewModdingAPI.Events.DayStartedEventArgs e)
        {
            foreach (Building building in Game1.getFarm().buildings)
            {
                this.Monitor.Log($"Checking Building {building.buildingType.Value}");
                if (building is Coop || building is Barn)
                {
                    AnimalHouse indoors = building.indoors.Value as AnimalHouse;
                    foreach (StardewValley.Object furniture in indoors.Objects.Values)
                    {
                        if (furniture.Name.Contains("arecrow"))
                        {
                            foreach (KeyValuePair<long, FarmAnimal> animal in indoors.animals.Pairs)
                            {
                                animal.Value.pet(Game1.MasterPlayer);
                                this.Monitor.Log("Pet animal");
                            }
                        }
                    }
                }
            }
        }
    }
}
