using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LethaboLegwaleRecipeAppPART2
{
    internal class Program
    {
        static List<Recipe> recipes = new List<Recipe>();

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the recipe app!");

            while (true)
            {
                Console.WriteLine("\nWhat would you like to do?");
                Console.WriteLine("1. Enter a new recipe");
                Console.WriteLine("2. Display all recipes");
                Console.WriteLine("3. Display a specific recipe");
                Console.WriteLine("4. Exit");
                //The switch case will select the specific method from the option the user
                //selected from the menu
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        EnterNewRecipe();
                        break;
                    case "2":
                        DisplayAllRecipes();
                        break;
                    case "3":
                        DisplaySpecificRecipe();
                        break;
                    case "4":
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 4.");
                        break;
                }
            }
        }
        //This method will allow the user to enter the new recipe with specifics
        //to ingredients and amounts
        static void EnterNewRecipe()
        {
            //The class recipe is called within the method
            Recipe recipe = new Recipe();

            Console.Write("Enter the name of the recipe: ");
            recipe.Name = Console.ReadLine();
            Console.Write("Enter the number of ingredients: ");
            int numIngredients = int.Parse(Console.ReadLine());
            //Below are the options provided to the user through a for loop to enter the ingridients for the user
            for (int i = 0; i < numIngredients; i++)
            {
                Console.Write($"Enter the name of ingredient {i + 1}: ");
                string name = Console.ReadLine();

                Console.Write($"Enter the quantity of {name}: ");
                double quantity = double.Parse(Console.ReadLine());

                Console.Write($"Enter the unit of measurement for {name}: ");
                string unit = Console.ReadLine();

                Console.Write($"Enter the number of calories for {name}: ");
                int calories = int.Parse(Console.ReadLine());

                Console.Write($"Enter the food group for {name}: ");
                string foodGroup = Console.ReadLine();

                recipe.AddIngredient(new Ingredient(name, quantity, unit, calories, foodGroup));
            }

            Console.Write("Enter the number of steps: ");
            int numSteps = int.Parse(Console.ReadLine());
            //for loop will display the amoiunt of steps you need to enter based on the amount the user inputed
            for (int i = 0; i < numSteps; i++)
            {
                Console.Write($"Enter step {i + 1}: ");
                recipe.AddStep(Console.ReadLine());
            }

            recipes.Add(recipe);
            Console.WriteLine("Recipe entered successfully!");
            //if the calories are above 300 the user will be notified
            if (recipe.CaloriesExceedLimit)
            {
                NotifyCaloriesExceeded(recipe);
            }
        }

        //This method will display all the recipes entered and then displays them
        static void DisplayAllRecipes()
        {
            if (recipes.Count == 0)
            {
                Console.WriteLine("No recipes found.");
                return;
            }

            Console.WriteLine("\nAll Recipes:");
            foreach (Recipe recipe in recipes.OrderBy(r => r.Name))
            {
                Console.WriteLine(recipe.Name);
            }
        }
        //This method will display the specific recipe the user would like to select

        static void DisplaySpecificRecipe()
        {
            //This if statement will check to see if there are recipes if not it will provide 
            //you with the menu to enter one if there is you will be able to select the recipe.
            if (recipes.Count == 0)
            {
                Console.WriteLine("THERE ARE NO EXSISTING RECIPES FOUND :)");
                return;
            }

            Console.WriteLine("\nSELECT A RECIPE TO DISPLAY:");
            for (int i = 0; i < recipes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {recipes[i].Name}");
            }

            Console.Write("Enter the number of the recipe: ");
            int recipeNumber = int.Parse(Console.ReadLine());

            if (recipeNumber >= 1 && recipeNumber <= recipes.Count)
            {
                Recipe selectedRecipe = recipes[recipeNumber - 1];
                selectedRecipe.Display();

                if (selectedRecipe.CaloriesExceedLimit)
                {
                    NotifyCaloriesExceeded(selectedRecipe);
                }
            }
            else
            {
                Console.WriteLine("Invalid recipe number.");
            }
        }
        //This notifies the user that the recipe exceeds the required amount of calories
        static void NotifyCaloriesExceeded(Recipe recipe)
        {
            Console.WriteLine($"Warning this recipe '{recipe.Name}' exceed 300 calories.");
        }
    }
    //Below are two classes Recipe and Ingredients they store the variables used within the methods
    class Recipe
    {
        public string Name { get; set; }
        private List<Ingredient> ingredients = new List<Ingredient>();
        private List<string> steps = new List<string>();

        public bool CaloriesExceedLimit
        {
            get { return CalculateTotalCalories() > 300; }
        }

        public void AddIngredient(Ingredient ingredient)
        {
            ingredients.Add(ingredient);
        }

        public void AddStep(string step)
        {
            steps.Add(step);
        }

        public void Display()
        {
            Console.WriteLine($"\nRecipe: {Name}");

            Console.WriteLine("\nIngredients:");
            foreach (Ingredient ingredient in ingredients)
            {
                Console.WriteLine($"- {ingredient.Name}, {ingredient.Quantity} {ingredient.Unit}");
            }

            Console.WriteLine("\nSteps:");
            for (int i = 0; i < steps.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {steps[i]}");
            }

            Console.WriteLine($"\nTotal Calories: {CalculateTotalCalories()}");
        }
        //The calories are calculated and then displayed 
        private int CalculateTotalCalories()
        {
            int totalCalories = 0;
            foreach (Ingredient ingredient in ingredients)
            {
                totalCalories += ingredient.Calories;
            }
            return totalCalories;
        }
    }

    class Ingredient
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }
        public int Calories { get; set; }
        public string FoodGroup { get; set; }

        public Ingredient(string name, double quantity, string unit, int calories, string foodGroup)
        {
            Name = name;
            Quantity = quantity;
            Unit = unit;
            Calories = calories;
            FoodGroup = foodGroup;
        }
    }
}
