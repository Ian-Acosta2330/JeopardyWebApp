// src/stores/players.js
import { writable } from 'svelte/store';

export const players = writable([]);
export const maxPlayers = 3;

/**
 * Adds a player to the list.
 * @param {string} name - The name of the player to add.
 */
export const addPlayer = (name) => {
    players.update(currentPlayers => {
        // Validate input
        if (
            currentPlayers.length < maxPlayers && // Check max player limit
            name.trim() !== '' && // Check non-empty name
            name.length <= 10 // Check max name length
        ) {
            let score = 0; // Initial score
            return [...currentPlayers, { name: name.trim(), score }];
        }
        return currentPlayers; // Return current state if invalid
    });
};

/**
 * Removes a player by name.
 * @param {string} name - The name of the player to remove.
 */
export const removePlayer = (name) => {
    players.update(currentPlayers => {
        return currentPlayers.filter(player => player.name !== name);
    });
};

/**
 * Updates the score of a player.
 * @param {string} name - The name of the player to update.
 * @param {number} points - The points to add or subtract.
 */
export const updateScore = (name, points) => {
    players.update((currentPlayers) => {
        const updatedPlayers = currentPlayers.map((player) =>
            player.name === name
                ? { ...player, score: player.score + points }
                : player
        );
        console.log('Updated Players:', updatedPlayers); // Log for debugging
        return updatedPlayers;
    });
};

