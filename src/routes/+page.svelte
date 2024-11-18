<title>JeopardyWebApp</title>
<script>
  import { players, addPlayer, removePlayer, updateScore, maxPlayers } from '../stores/players';
  import { goto } from '$app/navigation';

  let playerName = '';
  let message = '';

  const handleAddPlayer = () => {
      if (playerName.length > 10) {
          message = 'Name cannot exceed 10 characters.';
          return;
      }

      addPlayer(playerName);
      playerName = '';
      message = '';
  };



  const startGame = () => {
      if ($players.length > 0) {
          goto('/game');
      }
  };

  $: playersCount = $players.length;
</script>

<style>
  .container {
      display: flex;
      flex-direction: column;
      align-items: center;
      justify-content: center;
      height: 100vh;
      text-align: center;
  }

  .player-form {
      display: flex;
      flex-direction: column;
      align-items: center;
      margin-bottom: 20px;
  }

  .player-form input {
      margin: 10px;
      padding: 10px;
      width: 200px;
  }

  .player-form button {
      margin: 10px;
      padding: 10px 20px;
      cursor: pointer;
  }

  .player-list {
      list-style-type: none;
      padding: 0;
      margin-bottom: 20px;
  }

  .player-item {
      display: flex;
      align-items: center;
      justify-content: center;
      margin: 10px;
  }

  .player-item button {
      margin-left: 10px;
      cursor: pointer;
  }
</style>

<div class="container">
  <div class="player-form">
      <h2>Add Players</h2>
      <input
          type="text"
          bind:value={playerName}
          placeholder="Enter player name"
          maxlength="10"
      />
      <button on:click={handleAddPlayer} disabled={playersCount >= maxPlayers}>
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
