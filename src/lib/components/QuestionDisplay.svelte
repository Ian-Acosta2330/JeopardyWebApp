<script>
  import { createEventDispatcher, onMount } from 'svelte';

  export let question; // Accept the question prop
  export let timeLimit = 15; // Default time limit for answering (in seconds)
  const dispatch = createEventDispatcher();

  let userAnswer = ''; // To store the user's input
  let timer = timeLimit; // Countdown timer
  let interval; // Interval ID for the timer

  // Helper function to decode HTML entities
  function decodeHTML(text) {
    const parser = new DOMParser();
    return parser.parseFromString(text, 'text/html').documentElement.textContent;
  }

  // Submit the answer (either manually or automatically)
  function handleSubmit() {
    clearInterval(interval); // Stop the timer
    dispatch('submitAnswer', userAnswer); // Emit the user's answer
  }

  // Start the countdown timer
  function startTimer() {
    interval = setInterval(() => {
      timer -= 1;
      if (timer <= 0) {
        clearInterval(interval);
        handleSubmit(); // Auto-submit the answer when the timer reaches 0
      }
    }, 1000);
  }

  // Start the timer when the component is mounted
  onMount(() => {
    startTimer();
    return () => clearInterval(interval); // Cleanup the timer on component destruction
  });
</script>

<div class="modal">
  <div class="modal-content">
    <!-- Decode and display the question -->
    <h2>{decodeHTML(question.question)}</h2>

    <!-- Display remaining time -->
    <p class="timer">Time remaining: {timer} seconds</p>

    <!-- Input field for the user's answer -->
    <input
      type="text"
      bind:value={userAnswer}
      placeholder="Enter your answer"
    />

    <!-- Submit button -->
    <button on:click={handleSubmit}>Submit</button>
  </div>
</div>

<style>
  /* Modal styling */
  .modal {
    position: fixed;
    top: 0;
    left: 0;
    width: 100vw;
    height: 100vh;
    background-color: rgba(0, 0, 0, 0.6);
    display: flex;
    justify-content: center;
    align-items: center;
  }

  .modal-content {
    background: radial-gradient(circle, #004080, #001f40);
    color: white;
    padding: 2rem;
    border-radius: 10px;
    text-align: center;
    max-width: 500px;
    width: 90%;
    box-shadow: 0 4px 10px rgba(0, 0, 0, 0.25);
  }

  .timer {
    font-size: 1.2rem;
    color: #cc0000;
    margin-bottom: 1rem;
  }

  input {
    padding: 0.5rem;
    margin-top: 1rem;
    margin-bottom: 1rem;
    width: 100%;
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
