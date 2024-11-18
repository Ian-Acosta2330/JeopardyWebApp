<script>
    import { categories } from "/src/stores/categories.js";
    import { players, updateScore } from "/src/stores/players.js";
    import { get } from "svelte/store";
    import QuestionDisplay from "/src/components/QuestionDisplay.svelte";
  
    let selectedQuestion = null;
    let currentPlayerIndex = 0;
    let showModal = false; // Controls modal visibility
  
    // Reactive players and categories
    $: gamePlayers = $players; // Use $players directly
    $: categoryData = $categories; // Use $categories directly
  
    // Select a question from the board
    function selectQuestion(categoryIndex, questionIndex) {
      const question = categoryData[categoryIndex].questions[questionIndex];
      if (!question.answered) {
        selectedQuestion = { ...question, categoryIndex, questionIndex };
        showModal = true; // Show the modal
      }
    }
  
    // Submit an answer for the selected question
    function submitAnswer(event) {
      const answer = event.detail; // Extract the answer from the dispatched event
      const correctAnswer = selectedQuestion.correct_answer.toLowerCase();
      const player = gamePlayers[currentPlayerIndex];
  
      const points = (selectedQuestion.questionIndex + 1) * 100; // Calculate points
  
      if (answer.trim().toLowerCase() === correctAnswer) {
        alert("Correct!");
        updateScore(player.name, points); // Add points
      } else {
        alert(`Incorrect! The correct answer is: ${selectedQuestion.correct_answer}`);
        updateScore(player.name, -points); // Subtract points
      }
  
      // Mark question as answered
      categories.update((cats) => {
        cats[selectedQuestion.categoryIndex].questions[selectedQuestion.questionIndex].answered = true;
        return cats;
      });
  
      // Move to the next player and close the modal
      selectedQuestion = null;
      currentPlayerIndex = (currentPlayerIndex + 1) % gamePlayers.length;
      showModal = false; // Hide the modal
    }
  </script>
  
  <div class="game-container">
    <!-- Players Display -->
    <div class="players">
      {#each gamePlayers as player, i}
        <div class="player">
          <h3>
            {player.name}
            {i === currentPlayerIndex ? " (Your Turn)" : ""}
          </h3>
          <p>Score: {player.score}</p>
        </div>
      {/each}
    </div>
  
    <!-- Jeopardy Board -->
    <div class="board">
      {#each categoryData as { category, questions }, categoryIndex}
        <div class="category-container">
          <div class="category">{category}</div>
          {#each questions as question, questionIndex}
            <button
              on:click={() => selectQuestion(categoryIndex, questionIndex)}
              disabled={question.answered}
            >
              {question.answered ? "X" : (questionIndex + 1) * 100}
            </button>
          {/each}
        </div>
      {/each}
    </div>
  
    <!-- Modal for Question Display -->
    {#if showModal}
      <div class="modal-overlay">
        <div class="modal">
          <QuestionDisplay
            question={selectedQuestion}
            on:submitAnswer={submitAnswer}
          />
        </div>
      </div>
    {/if}
  </div>
  
  <style>
    .game-container {
      display: flex;
      flex-direction: column;
      align-items: center;
      color: white;
      gap: 2rem;
    }
  
    .players {
      display: flex;
      justify-content: space-evenly;
      width: 100%;
    }
  
    .player {
      text-align: center;
    }
  
    .board {
      display: grid;
      grid-template-columns: repeat(6, 1fr); /* Six columns for categories */
      gap: 1rem;
      width: 90%;
      margin: 0 auto;
    }
  
    .category-container {
      display: flex;
      flex-direction: column;
      align-items: center;
      gap: 0.5rem;
    }
  
    .category {
      text-align: center;
      font-weight: bold;
      margin-bottom: 0.5rem;
      color: #66ccff;
      text-shadow: 0 2px 4px rgba(0, 0, 0, 0.3);
    }
  
    button {
      background-color: #00509e;
      color: white;
      width: 100%;
      font-size: 1rem;
      padding: 1rem;
      border: none;
      border-radius: 0.5rem;
      box-shadow: 0 4px 6px rgba(0, 0, 0, 0.3);
      cursor: pointer;
      text-align: center;
    }
  
    button:disabled {
      background-color: #333;
      color: #777;
      cursor: not-allowed;
    }
  
    button:hover {
      background-color: #0074cc;
      transform: scale(1.05);
    }
  
    /* Modal Styles */
    .modal-overlay {
      position: fixed;
      top: 0;
      left: 0;
      width: 100vw;
      height: 100vh;
      background: rgba(0, 0, 0, 0.6);
      display: flex;
      justify-content: center;
      align-items: center;
    }
  
    .modal {
    background: radial-gradient(circle, #004080, #001f40);
    display:flex;
    justify-content: center;
    padding: 2rem;
    border-radius: 8px;
    box-shadow: 0 4px 10px rgba(0, 0, 0, 0.25);
    width: 90%;
    max-width: 500px;
    text-align: center;
    color: white; /* Match text color */
  }
  </style>
  