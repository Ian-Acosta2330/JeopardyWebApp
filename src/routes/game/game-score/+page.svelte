<script>
    import { players } from "$lib/stores/players";
    import { get } from "svelte/store";
    import { goto } from "$app/navigation";

    // Fetch the final scores from the players store
    const gamePlayers = get(players);

    // Determine the highest score
    const highestScore = Math.max(...gamePlayers.map(player => player.score));

    // Determine the winner(s)
    const winners = gamePlayers.filter(player => player.score === highestScore);

    function navigateToHome() {
        goto("/");
    }
</script>

<div class="game-score-container">
    <h2 class="title" style="font-weight:100">Final Scores</h2>

    <div class="scores">
        {#each gamePlayers as player (player.name)}
            <div
                class="player-score {player.score === highestScore ? 'winner' : ''}"
            >
                <h2 style="font-weight:100">{player.name}</h2>
                <p class="score">Score: ${player.score}</p>
                {#if player.score === highestScore}
                    <p class="winner-badge">Winner!</p>
                {/if}
            </div>
        {/each}
    </div>

    <button class="action-button" on:click={() => navigateToHome()}>
        Play Again
    </button>
</div>

<style>
    .game-score-container {
        display: flex;
        flex-direction: column;
        align-items: center;
        justify-content: center;
        height: 100vh;
        color: white;
        font-family: "Impact", sans-serif;
        font-weight:100;
        text-align: center;
        width:100%
    }

    .title {
        font-size: 3rem;
        
        margin-bottom: 2rem;
        text-shadow: 0 4px 6px rgba(0, 0, 0, 0.5);
    }

    .scores {
        display: grid;
        grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
        gap: 2rem;
        width: 80%;
    }

    .player-score {
        background: radial-gradient(circle, #004080, #001f40);
        padding: 1rem;
        border-radius: 10px;
        box-shadow: 0 4px 10px rgba(164, 164, 164, 0.3);
        text-align: center;
        transition: transform 0.2s ease-in-out;
        
    }
    .player-score:hover{
        transform: scale(1.05);
    }
    

    .player-score.winner {
        background: radial-gradient(circle, #004080, #001f40);
        box-shadow: 0 4px 15px rgba(234, 234, 233, 0.8);
        transition: transform 0.2s ease-in-out;
    }
    .player-score.winner:hover{
        transform: scale(1.05);
    }

    .player-score h2 {
        font-size: 1.5rem;
        margin-bottom: 0.5rem;
    }

    .player-score .score {
        font-size: 1.2rem;
    }

    .winner-badge {
        font-size: 1.2rem;
        color: gold;
        margin-top: 0.5rem;
    }

    .action-button {
        margin-top: 2rem;
        background-color: #cc0000;
        color: white;
        padding: 0.75rem 1.5rem;
        font-size: 1.2rem;
        font-weight: bold;
        border-radius: 8px;
        border: none;
        cursor: pointer;
        box-shadow: 0 4px 6px rgba(0, 0, 0, 0.3);
        transition: transform 0.2s ease-in-out;
    }

    .action-button:hover {
        background-color: #ff3333;
        transform: scale(1.05);
    }
</style>
