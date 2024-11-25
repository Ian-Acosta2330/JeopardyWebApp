<script>
    import { onMount } from "svelte";
    import { players, updateScore } from "$lib/stores/players";
    import { goto } from "$app/navigation";
    import { get } from "svelte/store";

    let finalQuestion = null; // Store the fetched question
    const gamePlayers = get(players); // Fetch players' data
    let playerWagers = []; // Store wagers for each player
    let playerAnswers = []; // Store answers for each player
    let allPlayersDone = false; // Flag to track if all players submitted

    // Initialize the wagers and answers arrays
    gamePlayers.forEach(() => {
        playerWagers.push(0);
        playerAnswers.push("");
    });

    // Function to decode HTML entities in the question text
    function decodeHTML(text) {
        const parser = new DOMParser();
        return parser.parseFromString(text, "text/html").documentElement.textContent;
    }

    // Fetch the final Jeopardy question
    onMount(async () => {
        try {
            const response = await fetch(
                "http://localhost:5041/api/Trivia/randomcategory/withquestions?amount=1"
            );

            if (!response.ok) {
                throw new Error("Failed to fetch the Final Jeopardy question.");
            }

            const data = await response.json();
            finalQuestion = data.questions[0];
        } catch (error) {
            console.error("Error fetching Final Jeopardy question:", error);
        }
    });

    // Submit all answers and wagers, then calculate results
    function finalizeRound() {
        const correctAnswer = finalQuestion.correct_answer.trim().toLowerCase();

        gamePlayers.forEach((player, index) => {
            const wager = playerWagers[index];
            const answer = playerAnswers[index].trim().toLowerCase();

            if (wager <= 0 || wager > player.score) {
                alert(
                    `${player.name} had an invalid wager. Skipping wager evaluation.`
                );
                return;
            }

            if (answer === correctAnswer) {
                updateScore(player.name, wager); // Add wager points
            } else {
                updateScore(player.name, -wager); // Deduct wager points
            }
        });

        allPlayersDone = true;
        goto("/game/game-score"); // Navigate to the final score page
    }
</script>

{#if finalQuestion}
    <div class="final-jeopardy-container">
        <h2 class="title">Final Jeopardy</h2>
        <div class="question">
            <p class="question-text">{decodeHTML(finalQuestion.question)}</p>
        </div>

        <!-- Input wagers and answers for all players -->
        <div class="players-section">
            {#each gamePlayers as player, index}
                <div class="player-entry">
                    <h3 class="player-name">{player.name}</h3>
                    <div class="wager-section">
                        <label for="wager-{index}" class="input-label">Your Wager:</label>
                        <input
                            type="number"
                            id="wager-{index}"
                            bind:value={playerWagers[index]}
                            class="input-field"
                            min="0"
                            max={player.score}
                        />
                    </div>
                    <div class="answer-section">
                        <label for="answer-{index}" class="input-label">Your Answer:</label>
                        <input
                            type="text"
                            id="answer-{index}"
                            bind:value={playerAnswers[index]}
                            class="input-field"
                        />
                    </div>
                </div>
            {/each}
        </div>

        <!-- Submit all answers -->
        <button class="action-button" on:click={finalizeRound}>
            Submit Final Answers
        </button>
    </div>
{:else}
    <div class="loading-container">
        <p class="loading-text">Loading Final Jeopardy...</p>
    </div>
{/if}

<style>
    .final-jeopardy-container {
        display: flex;
        flex-direction: column;
        align-items: center;
        justify-content: center;
        color: white;
        font-family: "Impact", sans-serif;
        text-align: center;
        height: 100vh;
        padding: 1rem;
    }

    .title {
        font-size: 2.5rem;
        text-shadow: 0 2px 4px rgba(0, 0, 0, 0.5);
        margin-bottom: 2rem;
    }

    .question-text {
        font-size: 1.8rem;
        color: #66ccff;
        text-shadow: 0 2px 4px rgba(0, 0, 0, 0.3);
        margin-bottom: 2rem;
    }

    .players-section {
        display: flex;
        flex-direction: row;
        gap: 2rem;
        width: 100%;
        max-width: 100%;
        margin: 0 auto;
    }

    .player-entry {
        background: radial-gradient(circle, #004080, #001f40);
        padding: 1rem;
        border-radius: 8px;
        box-shadow: 0 4px 6px rgba(0, 0, 0, 0.3);
        width:100%;
    }

    .player-name {
        font-size: 1.5rem;
        margin-bottom: 1rem;
    }

    .input-label {
        font-size: 1rem;
        margin-bottom: 0.5rem;
        display: block;
        color: white;
    }

    .input-field {
        width: 100%;
        padding: 0.5rem;
        font-size: 1rem;
        border-radius: 8px;
        border: 1px solid #ccc;
        margin-bottom: 1rem;
    }

    .action-button {
        margin-top: 2rem;
        background-color: #0074cc;
        color: white;
        padding: 0.75rem 1.5rem;
        font-size: 1rem;
        font-weight: bold;
        border-radius: 8px;
        border: none;
        cursor: pointer;
        box-shadow: 0 4px 6px rgba(0, 0, 0, 0.3);
        transition: transform 0.2s ease-in-out;
    }

    .action-button:hover {
        transform: scale(1.05);
    }

    .loading-container {
        display: flex;
        justify-content: center;
        align-items: center;
        height: 100vh;
        color: white;
        font-family: "Impact", sans-serif;
    }

    .loading-text {
        font-size: 1.5rem;
    }
</style>
