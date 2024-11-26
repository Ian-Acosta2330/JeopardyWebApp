<script>
  import PlayerSelect from '../../lib/components/PlayerSelect.svelte'
  import Scoreboard from '../../lib/components/Scoreboard.svelte'
  </script>

<style>
.player-select-container{
  justify-content: space-between;
  columns: 2;
}
</style>

<div class=player-select-container>
  <PlayerSelect />
  <Scoreboard />
</div>