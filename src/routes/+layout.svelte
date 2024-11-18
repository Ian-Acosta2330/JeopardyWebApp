<script>
    import { onMount } from 'svelte';
    import { categories } from '../stores/categories.js'; // Import the writable store
  
    let isLoading = true; // Flag to indicate the loading state
  
    onMount(async () => {
      try {
        const response = await fetch('http://localhost:5041/api/Trivia/randomcategories/withquestions?amount=5');
        if (!response.ok) throw new Error('Failed to fetch categories');
  
        const data = await response.json();
        categories.set(data); // Update the writable store
      } catch (error) {
        console.error('Error fetching categories:', error);
      } finally {
        isLoading = false; // Stop loading
      }
    });
  </script>
  
  <!-- Conditional UI -->
  {#if isLoading}
    <div class="loading-container">
      <div class="loading-animation">
        <h1>LOADING...</h1>
        <div class="spinner">
          <div class="bar"></div>
          <div class="bar"></div>
          <div class="bar"></div>
          <div class="bar"></div>
          <div class="bar"></div>
          <div class="bar"></div>
          <div class="bar"></div>
          <div class="bar"></div>
          <div class="bar"></div>
        </div>
        <p>Get ready for some trivia magic!</p>
      </div>
    </div>
  {:else}
    <slot /> <!-- Render the rest of the app -->
  {/if}
  
  <style>
    /* Loading Screen Styles */
    .loading-container {
      display: flex;
      flex-direction: column;
      justify-content: center;
      align-items: center;
      height: 100vh;
      width: 100dvw;
      background: radial-gradient(circle, #004080, #001f40);
      color: #f5f5f5;
      font-family: 'Impact', sans-serif;
      text-align: center;
      padding: 2rem;
    }
  
    .loading-animation {
      display: flex;
      flex-direction: column;
      align-items: center;
      gap: 1.5rem;
    }
  
    .loading-animation h1 {
      font-size: 3.5rem;
      animation: pulse 1.5s infinite;
      text-shadow: 0 4px 15px rgba(0, 0, 0, 0.7);
    }
  
    .loading-animation p {
      font-size: 1.5rem;
      margin-top: 1rem;
      opacity: 0.8;
      animation: blink 2s infinite;
    }
  
    /* Spinner */
    .spinner {
      display: grid;
      grid-template-columns: repeat(3, 1fr); /* 3x3 grid for square */
      gap: 0.5rem; /* Even spacing between bars */
      margin-top: 1rem;
      width: 6rem;
      height: 6rem;
    }
  
    .bar {
      width: 1.25rem;
      height: 1.25rem;
      background: #66ccff;
      animation: grow-shrink 1.2s infinite ease-in-out;
    }
  
    .bar:nth-child(1) { animation-delay: 0s; }
    .bar:nth-child(2) { animation-delay: 0.15s; }
    .bar:nth-child(3) { animation-delay: 0.3s; }
    .bar:nth-child(4) { animation-delay: 0.45s; }
    .bar:nth-child(5) { animation-delay: 0.6s; }
    .bar:nth-child(6) { animation-delay: 0.75s; }
    .bar:nth-child(7) { animation-delay: 0.9s; }
    .bar:nth-child(8) { animation-delay: 1.05s; }
    .bar:nth-child(9) { animation-delay: 1.2s; }
  
    /* Animations */
    @keyframes grow-shrink {
      0%, 100% {
        transform: scale(1);
        background: #66ccff;
      }
      50% {
        transform: scale(1.5);
        background: #00509e;
      }
    }
  
    @keyframes pulse {
      0%, 100% {
        transform: scale(1);
        color: #f5f5f5;
      }
      50% {
        transform: scale(1.1);
        color: #66ccff;
      }
    }
  
    @keyframes blink {
      0%, 100% {
        opacity: 1;
      }
      50% {
        opacity: 0.5;
      }
    }
  </style>
  