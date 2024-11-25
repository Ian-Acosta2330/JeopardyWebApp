<script>
  import { players, addPlayer, removePlayer, maxPlayers } from '../stores/players';
  import { goto } from '$app/navigation';

  let playerName = '';
  let message = '';

  // Add player logic
  const handleAddPlayer = () => {
    if (!playerName.trim()) { // Ensure the name is not empty
      message = 'Name cannot be empty.';
      return;
    }
    if (playerName.length > 10) {
      message = 'Name cannot exceed 10 characters.';
      return;
    }

    addPlayer(playerName.trim());
    playerName = '';
    message = '';
  };

  // Start game navigation
  const startGame = () => {
    if ($players.length > 0) {
      goto('/game'); // Navigates to the game
    }
  };

  $: playersCount = $players.length;
</script>

<style>
  .player-container {
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

  .player-form h2 {
    font-size: 2rem;
    color: #f7f7f7;
    text-shadow: 0 4px 10px rgba(0, 0, 0, 0.7);
    margin-bottom: 1.5rem;
  }

  .player-form input {
    padding: 10px;
    width: 100%;
    margin-bottom: 1rem;
    border: none;
    background: rgba(255, 255, 255, 0.1);
    color: white;
    border-radius: 6px;
    text-align: center;
    outline: none;
    font-size: 1rem;
  }

  .player-form input::placeholder {
    color: rgba(255, 255, 255, 0.6);
  }

  .player-form button {
    margin: 0.5rem 0;
    padding: 10px 20px;
    font-size: 1rem;
    font-weight: bold;
    color: white;
    background: #00509e;
    border: none;
    border-radius: 6px;
    box-shadow: 0 4px 10px rgba(0, 0, 0, 0.3);
    width: 100%;
    cursor: pointer;
    transition: transform 0.2s, background-color 0.2s;
  }

  .player-form button:hover {
    background: #0074cc;
    transform: scale(1.05);
  }

  .player-form p {
    color: #ff5050;
    margin-top: 10px;
    font-size: 0.9rem;
  }

  .player-list {
    list-style-type: none;
    padding: 0;
    margin: 1rem 0 2rem 0;
  }

  .player-item {
    display: flex;
    justify-content: space-between;
    align-items: center;
    margin: 10px 0;
    color: white;
    font-size: 1rem;
  }

  .player-item button {
    padding: 5px 10px;
    font-size: 0.9rem;
    color: white;
    background: #ff5050;
    border: none;
    border-radius: 4px;
    cursor: pointer;
    box-shadow: 0 2px 5px rgba(0, 0, 0, 0.3);
    transition: background-color 0.2s, transform 0.2s;
  }

  .player-item button:hover {
    background: #ff1a1a;
    transform: scale(1.1);
  }

  button:disabled {
    background: rgba(255, 255, 255, 0.2);
    color: rgba(255, 255, 255, 0.5);
    cursor: not-allowed;
  }
</style>

<div class="player-container">
  <div class="section">
    <div class="player-form">
      <h2>Add Players</h2>
      <input
        type="text"
        bind:value={playerName}
        placeholder="Enter player name"
        maxlength="10"
      />
      <button on:click={handleAddPlayer} disabled={!playerName.trim() || playersCount >= maxPlayers}>
        Add Player
      </button>
      {#if message}
        <p>{message}</p>
      {/if}
    </div>

    <ul class="player-list">
      {#each $players as { name, score }}
        <li class="player-item">
          {name} - Score: {score}
          <button on:click={() => removePlayer(name)}>Remove</button>
        </li>
      {/each}
    </ul>

    <div class="player-form">
      <button on:click={startGame} disabled={playersCount === 0}>
        Start Game
      </button>
    </div>
  </div>
</div>
