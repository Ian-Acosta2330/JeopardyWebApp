<script>
    import { onMount } from 'svelte';

    let scores = [];
    let error = null;

    onMount(async () => {
        try {
            scores = await getScores();
        } catch (err) {
            error = err.message;
        }
    });

    async function getScores() {
        const response = await fetch('http://localhost:5041/api/Score');
        return await response.json();
    }

</script>

<style>
    .scoreboard-container {
      display: flex;
      flex-direction: column;
      align-items: center;
      justify-content: center;
      height: 100vh;
      color: white;
      text-align: center;
      background: none;
    }
  
    .section {
      background: rgba(0, 0, 0, 0.4);
      padding: 2rem 3rem;
      border-radius: 10px;
      box-shadow: 0 8px 20px rgba(0, 0, 0, 0.5);
      max-width: 400px;
      width: 100%;
    }

  </style>
  
  <div class="scoreboard-container">
    <div class="section">
        <h2>Top 10 Scores</h2>
        {#if error}
            <p class="error">{error}</p>
        {:else}
            <ul>
                {#each scores as score}
                    <li>{score.player}: {score.value}</li>
                {/each}
            </ul>
        {/if}
    </div>
  </div>
  