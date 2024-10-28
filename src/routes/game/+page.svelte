<title>Game Page</title>

<!-- src/routes/board.svelte -->
<script>
    import { question, fetchQuestion } from '../../stores/questions.js';
    import { onMount } from 'svelte';

    let showAnswer = false;

    onMount(async () => {
        await fetchQuestion();
    });

    function revealAnswer() {
        showAnswer = true;
    }
</script>

<style>
    /* Add some basic styling */
</style>

<div class="board">
    {#if $question}
        <h2>Question:</h2>
        <p>{$question.chosenQuestion}</p>
        {#if showAnswer}
            <h2>Answer:</h2>
            <p>{$question.chosenAnswer}</p>
        {:else}
            <button on:click={revealAnswer}>Reveal Answer</button>
        {/if}
    {:else}
        <p>Loading question...</p>
    {/if}
    <button on:click={() => fetchQuestion()}>Next Question</button>
</div>