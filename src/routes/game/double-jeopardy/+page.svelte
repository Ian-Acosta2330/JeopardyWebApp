<script>
    import Jeopardy from "$lib/components/Jeopardy.svelte";
    import { currentCategories } from "$lib/stores/categories.js"; // Import the correct store
    import { onDestroy } from "svelte";
  
    let loaded = false;
    let categoryData = [];
    let unsubscribe;
  
    // Subscribe to currentCategories store
    unsubscribe = currentCategories.subscribe((data) => {
      console.log("Subscribed to current categories:", data); // Debugging
      categoryData = data;
      loaded = categoryData.length > 0; // Mark as loaded if data exists
    });
  
    // Cleanup subscription on destroy
    onDestroy(() => {
      if (unsubscribe) unsubscribe();
    });
  </script>
  
  <!-- Render Jeopardy component if loaded -->
  {#if loaded}
    <Jeopardy phase="double-jeopardy" />
  {:else}
    <div class="loading-screen">
      <p>Loading Double Jeopardy...</p>
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
  