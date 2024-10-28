import { writable } from 'svelte/store';

export const question = writable(null);

export async function fetchQuestion() {
    const res = await fetch('http://localhost:5041/jeopardy');
    if (res.ok) {
        const data = await res.json();
        question.set(data);
    } else {
        console.error("Failed to fetch the question");
    }
}