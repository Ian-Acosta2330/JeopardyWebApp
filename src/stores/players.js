// src/stores/players.js
import { writable } from 'svelte/store';

export const players = writable([
    { name: "Player 1", score: 0 },
    { name: "Player 2", score: 0 },
    { name: "Player 3", score: 0}
]);
