import { writable } from 'svelte/store';

// Store for the categories and questions
export const categories = writable([]);

// Fetch categories and populate the store
export async function fetchCategories() {
    try {
        const response = await fetch('http://localhost:5041/api/Trivia/randomcategories/withquestions?amount=5');
        const data = await response.json();
        categories.set(data); // Update the store with fetched data
    } catch (error) {
        console.error('Error fetching categories:', error);
    }
}
