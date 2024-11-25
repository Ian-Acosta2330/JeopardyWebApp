import { writable } from "svelte/store";

export const currentCategories = writable([]); // Current round categories
export const nextRoundCategories = writable([]); // Preloaded categories for next round

// Function to fetch categories for the current round
export async function fetchCurrentCategories() {
  try {
    console.log("Fetching current categories...");
    const response = await fetch("http://localhost:5041/api/Trivia/randomcategories/withquestions?amount=5");
    if (!response.ok) {
      throw new Error("Failed to fetch current categories");
    }

    const data = await response.json();
    console.log("Fetched Current Categories:", data);
    currentCategories.set(data);
  } catch (error) {
    console.error("Error fetching current categories:", error);
    currentCategories.set([]); // Set an empty array to avoid infinite loading
  }
}

// Function to preload categories for the next round
export async function preloadNextRoundCategories() {
  try {
    console.log("Preloading next round categories...");
    const response = await fetch("http://localhost:5041/api/Trivia/randomcategories/withquestions?amount=5");
    if (!response.ok) {
      throw new Error("Failed to fetch next round categories");
    }

    const data = await response.json();
    console.log("Fetched Next Round Categories:", data);

    // Modify the points for Double Jeopardy
    const updatedCategories = data.map((category) => ({
      ...category,
      questions: category.questions.map((q, index) => ({
        ...q,
        points: (index + 1) * 200, // Double the point values for Double Jeopardy
        answered: false, // Reset answered state
      })),
    }));

    nextRoundCategories.set(updatedCategories); // Preload data for the next round
    console.log("Updated Next Round Categories:", updatedCategories);
  } catch (error) {
    console.error("Error preloading next round categories:", error);
    nextRoundCategories.set([]); // Set an empty array to avoid infinite loading
  }
}
