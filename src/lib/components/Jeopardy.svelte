<script>
  import { onMount } from "svelte";
  import {
    currentCategories,
    nextRoundCategories,
    preloadNextRoundCategories,
  } from "../stores/categories.js";
  import { players, updateScore } from "../stores/players.js";
  import { get } from "svelte/store";
  import { goto } from "$app/navigation";
  import QuestionDisplay from "./QuestionDisplay.svelte";

  export let phase = "jeopardy"; // Default phase
  let selectedQuestion = null;
  let currentPlayerIndex = 0;
  let showModal = false;

  // Reactive stores
  $: gamePlayers = $players;
  $: categoryData = $currentCategories; // Reactive to currentCategories store
  $: nextRoundData = $nextRoundCategories;

  // Preload next round categories logic
  async function ensureNextRoundPreloaded() {
    if (!nextRoundData || nextRoundData.length < 6) { // Adjust size based on expected category count
      console.log("Preloading next round categories...");
      try {
        await preloadNextRoundCategories();
        console.log("Next round categories preloaded successfully.");
      } catch (error) {
        console.error("Error preloading next round categories:", error);
      }
    } else {
      console.log("Next round categories are already preloaded.");
    }
  }

  // Preload next round's categories once at mount
  onMount(async () => {
    if (categoryData.length > 0) {
      await ensureNextRoundPreloaded();
    }
  });

  // Check if all questions are answered
  $: {
    const totalQuestions = categoryData.reduce(
      (total, category) => total + category.questions.length,
      0
    );
    const answeredQuestions = categoryData.reduce(
      (total, category) => total + category.questions.filter((q) => q.answered).length,
      0
    );

    // If all questions are answered, navigate to the next phase
    if (answeredQuestions === totalQuestions && totalQuestions > 0) {
      transitionToNextPhase();
    }
  }

  // Select a question
  function selectQuestion(categoryIndex, questionIndex) {
    const question = categoryData[categoryIndex]?.questions[questionIndex];
    if (question && !question.answered) {
      selectedQuestion = { ...question, categoryIndex, questionIndex };
      showModal = true; // Open modal for question display
    }
  }

  // Handle question answer submission
  function submitAnswer(event) {
    const answer = event.detail.trim().toLowerCase();
    const correctAnswer = selectedQuestion.correct_answer.trim().toLowerCase();
    const player = gamePlayers[currentPlayerIndex];
    const multiplier = phase === "double-jeopardy" ? 2 : 1;
    const points = (selectedQuestion.questionIndex + 1) * 100 * multiplier;

    if (answer === correctAnswer) {
      alert("Correct!");
      updateScore(player.name, points); // Award points
    } else {
      alert(`Incorrect! The correct answer is: ${selectedQuestion.correct_answer}`);
      updateScore(player.name, -points); // Deduct points
    }

    // Mark question as answered
    currentCategories.update((cats) => {
      cats[selectedQuestion.categoryIndex].questions[selectedQuestion.questionIndex].answered = true;
      return cats;
    });

    // Prepare for the next player and close modal
    selectedQuestion = null;
    currentPlayerIndex = (currentPlayerIndex + 1) % gamePlayers.length;
    showModal = false;
  }

  // Transition to the next phase
  function transitionToNextPhase() {
    const nextPhaseCategories = get(nextRoundCategories);

    if (phase === "jeopardy") {
      if (nextPhaseCategories.length > 0) {
        currentCategories.set(nextPhaseCategories); // Set Double Jeopardy categories
        goto("/game/double-jeopardy"); // Navigate to Double Jeopardy
      } else {
        console.error("Next round categories not preloaded");
      }
    } else if (phase === "double-jeopardy") {
      goto("/game/final-jeopardy"); // Navigate to Final Jeopardy
    }
  }
</script>

<div class="game-container">
  <!-- Player Scores -->
  <div class="players">
    {#each gamePlayers as player, i}
      <div class="player">
        <h3>
          {player.name} {i === currentPlayerIndex ? " (Your Turn)" : ""}
        </h3>
        <p>Score: {player.score}</p>
      </div>
    {/each}
  </div>

  <!-- Game Board -->
  <div class="board">
    {#if categoryData.length > 0}
      {#each categoryData as { category, questions }, categoryIndex}
        <div class="category-container">
          <div class="category">{category}</div>
          {#each questions as question, questionIndex}
            <button
              on:click={() => selectQuestion(categoryIndex, questionIndex)}
              disabled={question.answered}
            >
              {question.answered ? "X" : (questionIndex + 1) * 100 * (phase === "double-jeopardy" ? 2 : 1)}
            </button>
          {/each}
        </div>
      {/each}
    {:else}
      <p>Loading questions...</p>
    {/if}
  </div>

  <!-- Finish Round Button -->
  <div class="finish-round">
    <button on:click={transitionToNextPhase}>Finish Round</button>
  </div>

  <!-- Question Modal -->
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
    align-items: end;
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

  /* Finish Round Button */
  .finish-round {
    margin-top: 1rem;
    display: flex;
    justify-content: center;
  }

  .finish-round button {
    background-color: #cc0000;
    color: white;
    padding: 0.75rem 1.5rem;
    font-size: 1.2rem;
    font-weight: bold;
    border-radius: 0.5rem;
    border: none;
    cursor: pointer;
    box-shadow: 0 4px 6px rgba(0, 0, 0, 0.3);
  }

  .finish-round button:hover {
    background-color: #ff3333;
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
    display: flex;
    justify-content: center;
    padding: 2rem;
    border-radius: 8px;
    box-shadow: 0 4px 10px rgba(0, 0, 0, 0.25);
    width: 90%;
    max-width: 500px;
    text-align: center;
    color: white;
  }
</style>
