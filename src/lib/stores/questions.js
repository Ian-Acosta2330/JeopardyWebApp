import { writable } from 'svelte/store';

export const categories = writable([]); // Store for all categories and their questions

export async function fetchCategories() {
    try {
        const res = await fetch('http://localhost:5041/jeopardy');
        if (res.ok) {
            const data = await res.json();

            // Format the categories and add points to each question
            const formattedCategories = data.map(category => ({
                ...category,
                questions: category.questions.map((q, index) => ({
                    ...q,
                    points: (index + 1) * 100, // Assign points based on question index
                    answered: false,          // Ensure all questions start unanswered
                })),
            }));

            categories.set(formattedCategories); // Update the categories store
        } else {
            console.error("Failed to fetch categories");
        }
    } catch (error) {
        console.error("Error fetching categories:", error);
    }
}
