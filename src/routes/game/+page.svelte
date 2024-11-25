<script>
  import { goto } from '$app/navigation';
  import { onMount } from 'svelte';

  let loading = true;

  // Simulate loading and navigate to Jeopardy
  onMount(() => {
    const loadingTime = 2000; // 2 seconds
    setTimeout(() => {
      loading = false;
      goto('/game/jeopardy'); // Navigate to Jeopardy phase
    }, loadingTime);
  });
</script>

{#if loading}
  <div class="loading-screen">
    <div class="spinner"></div>
    <p>Loading Jeopardy...</p>
  </div>
{/if}

<style>
  .loading-screen {
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    height: 100vh;
    color: white;
    font-family: Arial, sans-serif;
  }

  .spinner {
    width: 50px;
    height: 50px;
    border: 5px solid rgba(255, 255, 255, 0.2);
    border-top-color: white;
    border-radius: 50%;
    animation: spin 1s linear infinite;
    margin-bottom: 1rem;
  }

  @keyframes spin {
    to {
      transform: rotate(360deg);
    }
  }

  p {
    font-size: 1.2rem;
    margin-top: 1rem;
  }
</style>
