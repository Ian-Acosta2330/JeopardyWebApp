<script>
    import { onMount } from "svelte";
    import { updateScore } from "$lib/stores/players.js";
  
    let wager = 0; // Wager input by the user
    let answer = ""; // Answer input by the user
    let finalQuestion = null; // Stores the fetched final question
    let isLoading = true; // Tracks if the question is being loaded
  
    // Fetch the final question from the API
    onMount(async () => {
      try {
        const response = await fetch(
          "http://localhost:5041/api/Trivia/randomcategory/withquestions?amount=1"
        );
        if (!response.ok) {
          throw new Error("Failed to load final question");
        }
  
        const data = await response.json();
        finalQuestion = data.questions[0]; // Assuming the API response wraps the question in an array
      } catch (error) {
        console.error("Error fetching final question:", error);
      } finally {
        isLoading = false; // Stop loading spinner
      }
    });
  
    // Handle wager submission
    function submitWager() {
      if (wager <= 0) {
        alert("Please enter a valid wager!");
        return;
      }
      alert(`You wagered: ${wager}`);
    }
  
    // Handle final answer submission
    function submitFinalAnswer() {
      if (!answer.trim()) {
        alert("Please enter an answer!");
        return;
      }
  
      const correctAnswer = finalQuestion.correct_answer.trim().toLowerCase();
      if (answer.trim().toLowerCase() === correctAnswer) {
        alert("Correct! Well done.");
        updateScore("Player Name", wager); // Replace "Player Name" with dynamic player logic
      } else {
        alert(`Incorrect. The correct answer is: ${finalQuestion.correct_answer}`);
        updateScore("Player Name", -wager); // Deduct wager on incorrect answer
      }
  
      // Reset for the next phase
      wager = 0;
      answer = "";
    }
  </script>
  
  {#if isLoading}
    <div class="loading-screen">
      <p>Loading Final Jeopardy...</p>
    </div>
  {:else if finalQuestion}
    <div class="final-jeopardy">
      <h2>Final Jeopardy</h2>
      <div class="question">
        <p>{finalQuestion.question}</p>
      </div>
      <div class="wager">
        <label for="wager">Enter your wager:</label>
        <input type="number" id="wager" bind:value={wager} />
        <button on:click={submitWager}>Submit Wager</button>
      </div>
      <div class="answer">
        <label for="answer">Enter your answer:</label>
        <input type="text" id="answer" bind:value={answer} />
        <button on:click={submitFinalAnswer}>Submit Answer</button>
      </div>
    </div>
  {:else}
    <div class="error-screen">
      <p>Failed to load Final Jeopardy question.</p>
    </div>
  {/if}
  
  <style>
    .loading-screen,
    .error-screen {
      display: flex;
      flex-direction: column;
      justify-content: center;
      align-items: center;
      height: 100vh;
      background-color: #001f40;
      color: white;
      font-family: Arial, sans-serif;
    }
  
    .final-jeopardy {
      text-align: center;
      color: white;
      font-family: Arial, sans-serif;
    }
  
    .question {
      margin: 2rem 0;
      font-size: 1.5rem;
    }
  
    .wager,
    .answer {
      margin: 1rem 0;
    }
  
    label {
      font-size: 1.2rem;
    }
  
    input {
      margin: 0.5rem;
      padding: 0.5rem;
      font-size: 1rem;
      border-radius: 4px;
      border: 1px solid #ccc;
    }
  
    button {
      background-color: #00509e;
      color: white;
      padding: 0.5rem 1rem;
      font-size: 1rem;
      border: none;
      border-radius: 4px;
      cursor: pointer;
    }
  
    button:hover {
      background-color: #0074cc;
    }
  </style>
  