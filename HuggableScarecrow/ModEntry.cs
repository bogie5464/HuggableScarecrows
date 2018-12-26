using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Buildings;
using StardewValley.Objects;
using StardewValley.Locations;

namespace HuggableScarecrow
{
    public class ModEntry : Mod
    {
        public override void Entry(IModHelper helper)
        {
            helper.Events.GameLoop.DayStarted += GameLoop_DayStarted;
        }

        private void GameLoop_DayStarted(object sender, StardewModdingAPI.Events.DayStartedEventArgs e)
        {
            this.Monitor.Log("Starting Day, checking buildings");
            foreach (Building building in Game1.getFarm().buildings)
            {
                this.Monitor.Log($"Checking Building {building.buildingType.Value}");
                if (building is Coop || building is Barn)
                {
                    this.Monitor.Log("Building was a coop/barn moving on");
                    //var getIndoors = Helper.Reflection.GetMethod(building, "getIndoors");
                    //GameLocation bldgIndoors = getIndoors.Invoke<GameLocation>(building.nameOfIndoorsWithoutUnique);
                    AnimalHouse indoors = building.indoors.Value as AnimalHouse;
                    foreach (StardewValley.Object furniture in indoors.Objects.Values)
                    {
                        this.Monitor.Log($"Checking object {furniture.Name}");
                        if (furniture.Name.Contains("arecrow"))
                        {
                            this.Monitor.Log("Building Contained Scarecrow");
                            foreach (KeyValuePair<long, FarmAnimal> animal in indoors.animals.Pairs)
                            {
                                this.Monitor.Log("Found animals");
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
