<script>
    import { onMount } from "svelte";

    let finalQuestion = null; // To store the fetched question
    let wager = 0; // User's wager
    let answer = ""; // User's answer

    onMount(async () => {
        try {
            // Fetch a single question from the API
            const response = await fetch(
                "http://localhost:5041/api/Trivia/randomcategory/withquestions?amount=1"
            );

            if (!response.ok) {
                throw new Error("Failed to fetch the Final Jeopardy question.");
            }

            const data = await response.json();

            // Assuming the API returns an array with a single category and its questions
            finalQuestion = data.questions[0];
        } catch (error) {
            console.error("Error fetching Final Jeopardy question:", error);
        }
    });

    function submitWager() {
        if (wager <= 0) {
            alert("Please enter a valid wager!");
            return;
        }
        alert(`You wagered: ${wager}`);
    }

    function submitFinalAnswer() {
        if (!answer.trim()) {
            alert("Please enter an answer!");
            return;
        }

        const correctAnswer = finalQuestion.correct_answer.trim().toLowerCase();
        if (answer.trim().toLowerCase() === correctAnswer) {
            alert("Correct! Well done.");
            // Add logic to update the score positively with the wager
        } else {
            alert(`Incorrect. The correct answer is: ${finalQuestion.correct_answer}`);
            // Add logic to deduct the wager from the score
        }

        // Reset for potential reuse
        wager = 0;
        answer = "";
    }
</script>

{#if finalQuestion}
    <div class="final-jeopardy-container">
        <h2 class="title">Final Jeopardy</h2>
        <div class="question">
            <p class="question-text">{finalQuestion.question}</p>
        </div>
        <div class="wager-section">
            <label for="wager" class="input-label">Your Wager:</label>
            <input
                type="number"
                id="wager"
                bind:value={wager}
                class="input-field"
            />
            <button class="action-button" on:click={submitWager}>
                Submit Wager
            </button>
        </div>
        <div class="answer-section">
            <label for="answer" class="input-label">Your Answer:</label>
            <input
                type="text"
                id="answer"
                bind:value={answer}
                class="input-field"
            />
            <button class="action-button" on:click={submitFinalAnswer}>
                Submit Answer
            </button>
        </div>
    </div>
{:else}
    <div class="loading-container">
        <p class="loading-text">Loading Final Jeopardy...</p>
    </div>
{/if}

<style>
    html, body {
        height: 100%;
        margin: 0;
        overflow: none; /* Prevent scrolling */
        overflow-y:none;
    }

    .final-jeopardy-container {
        display: flex;
        flex-direction: column;
        justify-content: center; /* Center all content vertically */
        align-items: center;
        color: white;
        font-family: "Impact", sans-serif;
        text-align: center;
        height: 100vh; /* Take up the entire viewport */
        padding: 1rem;
        box-sizing: border-box;
    }

    .title {
        font-size: 2.5rem;
        text-shadow: 0 2px 4px rgba(0, 0, 0, 0.5);
        margin-bottom: 2rem;
    }

    .question {
        margin-bottom: 2rem;
    }

    .question-text {
        font-size: 1.8rem;
        color: #66ccff;
        text-shadow: 0 2px 4px rgba(0, 0, 0, 0.3);
    }

    .wager-section,
    .answer-section {
        display: flex;
        flex-direction: column;
        align-items: center;
        margin-bottom: 1.5rem;
        width: 100%;
    }

    .input-label {
        font-size: 1.2rem;
        margin-bottom: 0.5rem;
        color: #f5f5f5;
    }

    .input-field {
        width: 80%;
        max-width: 400px;
        padding: 0.5rem;
        font-size: 1rem;
        border-radius: 8px;
        border: 1px solid #ccc;
        margin-bottom: 1rem;
    }

    .action-button {
        background-color: #00509e;
        color: white;
        padding: 0.5rem 1.5rem;
        font-size: 1rem;
        font-weight: bold;
        border: none;
        border-radius: 8px;
        cursor: pointer;
        box-shadow: 0 4px 6px rgba(0, 0, 0, 0.3);
        transition: transform 0.2s ease-in-out;
    }

    .action-button:hover {
        background-color: #0074cc;
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
