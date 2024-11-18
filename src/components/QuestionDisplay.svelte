<script>
  import { createEventDispatcher } from 'svelte';

  export let question; // Accept the question prop
  const dispatch = createEventDispatcher();

  let userAnswer = ''; // To store the user's input

  // Helper function to decode HTML entities
  function decodeHTML(text) {
    const parser = new DOMParser();
    return parser.parseFromString(text, 'text/html').documentElement.textContent;
  }

  function handleSubmit() {
    console.log("Correct Answer:", question.correct_answer); // Log the correct answer
    console.log("Player Answer:", userAnswer); // Log the player's answer
    dispatch('submitAnswer', userAnswer); // Emit only the answer as a string
  }
</script>

<div class="question-display">
  <!-- Decode HTML entities in the question -->
  <h2>{decodeHTML(question.question)}</h2>

  <input
    type="text"
    bind:value={userAnswer}
    placeholder="Enter your answer"
  />

  <button on:click={handleSubmit}>
    Submit
  </button>
</div>

<style>
  .question-display {
    text-align: center;
    margin-top: 2rem;
  }

  input {
    padding: 0.5rem;
    margin-top: 1rem;
    margin-bottom: 1rem;
    border: 1px solid #ccc;
    border-radius: 4px;
  }

  button {
    padding: 0.5rem 1rem;
    background-color: #00509e;
    color: white;
    border: none;
    border-radius: 4px;
    cursor: pointer;
  }

  button:hover {
    background-color: #0074cc;
  }
</style>
