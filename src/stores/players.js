// src/stores/players.js
import { writable } from 'svelte/store';

export const players = writable([]);
export const maxPlayers = 3;

export const addPlayer = (name) => {
    players.update(currentPlayers => {
        if (currentPlayers.length < maxPlayers && name.length <= 10) {
            let score = 0
            return [...currentPlayers, { name, score }]
        }
        return currentPlayers
    })
}

export const removePlayer = (name) => {
    players.update(currentPlayers => {
        return currentPlayers.filter(player => player.name !== name)
    })
}


export const updateScore = (name, points) => {
    players.update(currentPlayers => {
        return currentPlayers.map(player => 
            player.name === name ? { ...player, score: player.score + points } : player
        )
    })
}
