<script>
    import { onMount } from 'svelte';
    import { goto } from '$app/navigation';
  
    // Handle keypress to navigate
    function handleKeypress(event) {
      if (event.key === 'Enter') {
        goto('/player-select'); // Navigate to the player/gamemode select page
      }
    }
  
    // Add a global keypress listener when the component mounts
    onMount(() => {
      window.addEventListener('keydown', handleKeypress);
  
      return () => {
        window.removeEventListener('keydown', handleKeypress);
      };
    });
  </script>
  
  <style>
    /* Main container styles */
    main {
      display: flex;
      flex-direction: column;
      justify-content: center;
      align-items: center;
      height: 100dvh;
      width:100dvw;
      background: radial-gradient(circle, #004080, #001f40); /* Matches global background */
      font-family: 'Impact', sans-serif;
      color: #f5f5f5;
      overflow: hidden;
    }
  
    /* Floating animated Jeopardy title */
    h1 {
      font-size: 7rem; /* Slightly larger */
      color: #f5f5f5; /* Bright blue consistent with global.css */
      text-shadow: 0 4px 15px rgba(0, 0, 0, 0.7); /* Soft depth shadow */
      animation: float 3s ease-in-out infinite;
      margin-bottom: 1.5rem; /* Add space between title and text */
    }
  
    @keyframes float {
      0%, 100% {
        transform: translateY(0);
      }
      50% {
        transform: translateY(-10px);
      }
    }
  
    /* Press Enter text styles */
    p {
      font-size: 1.8rem; /* Slightly larger for readability */
      color: rgba(255, 255, 255, 0.623); /* Softer white */
      text-shadow: 0 2px 5px rgba(0, 0, 0, 0.5); /* Subtle shadow */
      animation: blink 1.5s infinite;
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
  
  <main>
    <h1>JEOPARDY</h1>
    <p>Press Enter to Start</p>
  </main>
  