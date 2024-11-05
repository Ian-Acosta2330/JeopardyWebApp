<script>
  export let category;
  export let questionData;
  let showHint = false;
  let userInput = "";

  function revealHint() {
    showHint = true;
  }

  function submitAnswer() {
    if (userInput.trim().toLowerCase() === questionData.answer.toLowerCase()) {
      alert("Correct!");
    } else {
      alert("Incorrect! The correct answer is: " + questionData.answer);
    }
  }
</script>

<div class="question-section">
  <h3>{questionData.question}</h3>
  
  {#if showHint}
    <p><strong>Hint:</strong> {questionData.hint}</p>
  {:else}
    <button on:click={revealHint}>Show Hint</button>
  {/if}

  <input type="text" bind:value={userInput} placeholder="Your answer" />
  <button on:click={submitAnswer}>Submit Answer</button>
</div>