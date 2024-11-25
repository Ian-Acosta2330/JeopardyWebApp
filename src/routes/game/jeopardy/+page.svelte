<script>
    import Jeopardy from "$lib/components/Jeopardy.svelte";
    import { currentCategories } from "$lib/stores/categories.js"; // Use the correct export
    import { onDestroy } from "svelte";
  
    let loaded = false;
    let categoryData = [];
    let unsubscribe;
  
    unsubscribe = currentCategories.subscribe((data) => {
      console.log("Subscribed current categories data:", data); // Debugging output
      categoryData = data;
      loaded = categoryData.length > 0; // Mark as loaded if data exists
    });
  
    // Cleanup subscription
    onDestroy(() => {
      if (unsubscribe) unsubscribe();
    });
  </script>
  
  {#if loaded}
    <Jeopardy phase="jeopardy" />
  {:else}
    <div class="loading-screen">
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
  
    p {
      font-size: 1.5rem;
      margin-top: 1rem;
    }
  </style>
  