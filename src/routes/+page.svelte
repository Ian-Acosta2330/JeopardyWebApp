<title>JeopardyWebApp</title>

<script>
  import Taskbar from '/src/components/taskbar.svelte';
  import QuestionDisplay from '/src/components/QuestionDisplay.svelte';
  import { onMount } from 'svelte';
  import { players } from '/src/stores/players.js';


  const items = [];

  let userInput = '';
  let result = '';

  let questions = {
      geography: [],
      math: [],
      history: []
  };

  let selectedQuestion = null;
  let selectedPlayer = 0; // Track which player's turn it is

  //Select random question based on the catagory and points
  function selectRandomQuestion(category, points) {
      const filteredQuestions = questions[category].filter(q => q.points === points && !q.answered);
      if (filteredQuestions.length > 0) {
          const randomIndex = Math.floor(Math.random() * filteredQuestions.length);
          selectedQuestion = { ...filteredQuestions[randomIndex], category };  // Set the category as well
      } else {
          alert("All questions in this category for this point value have been answered.");
      }
  }


  onMount(async () => {
      try {
          const response = await fetch('http://localhost:5041/jeopardy');
          if (!response.ok) {
              throw new Error('Network response was not ok');
          }
          const data = await response.json();
          questions = data;
      } catch (error) {
          console.error('Error fetching questions:', error);
      }
  });

  function handleSubmit() {
        if (userInput.trim().toLowerCase() === selectedQuestion.answer.toLowerCase()) { 
            alert("Correct!");
            updateScore(selectedPlayer, selectedQuestion.points); // Update score if correct
        } else {
            alert("Incorrect! The correct answer is: " + selectedQuestion.answer);
            updateScore(selectedPlayer, -selectedQuestion.points); // Deduct points if incorrect
        }
        markQuestionAsAnswered(selectedQuestion.category, selectedQuestion.points);
        selectedQuestion = null; 
        userInput = ""; 
    }

  function markQuestionAsAnswered(category, points) {
      const questionIndex = questions[category].findIndex(q => q.points === points && !q.answered);
      if(questions[category][questionIndex].answered) {
        location.reload();
      }
      if (questionIndex !== -1) {
          questions[category][questionIndex].answered = true;
      }
  }
  // Update player score
  function updateScore(playerIndex, points) {
        players.update(plrs => {
            plrs[playerIndex].score += points;
            return plrs;
        });
    }

  
</script>

<style>
  .players {
      display: flex;
      justify-content: space-around;
      padding: 10px;
  }
  .player {
      text-align: center;
  }
</style>

<Taskbar {items} />

<h1 class="heading"> Jeopardy Main </h1>

<!-- Display Players and Scores -->
<div class="players">
  {#each $players as player, i}
      <div class="player">
          <h3>{player.name}</h3>
          <p>Score: {player.score}</p>
      </div>
  {/each}
</div>

<!-- Game Board -->
<div class="prompts">
  <div class="container">
      <table>
          <thead>
            <tr>
              <th>Geography</th>
              <th>Math</th>
              <th>History</th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <td>
                {#if !selectedQuestion || selectedQuestion.category !== 'geography'}
                  <button on:click={() => selectRandomQuestion('geography', '100')} disabled={questions.geography.filter(q => q.points === '100' && !q.answered).length === 0}>
                    100
                  </button>
                {:else if selectedQuestion.points === '100'}
                  <QuestionDisplay category="geography" questionData={selectedQuestion} />
                {/if}
              </td>
              <td>
                {#if !selectedQuestion || selectedQuestion.category !== 'math'}
                  <button on:click={() => selectRandomQuestion('math', '100')} disabled={questions.math.filter(q => q.points === '100' && !q.answered).length === 0}>
                    100
                  </button>
                {:else if selectedQuestion.points === '100'}
                  <QuestionDisplay category="math" questionData={selectedQuestion} />
                {/if}
              </td>
              <td>
                {#if !selectedQuestion || selectedQuestion.category !== 'history'}
                  <button on:click={() => selectRandomQuestion('history', '100')} disabled={questions.history.filter(q => q.points === '100' && !q.answered).length === 0}>
                    100
                  </button>
                {:else if selectedQuestion.points === '100'}
                  <QuestionDisplay category="history" questionData={selectedQuestion} />
                {/if}
              </td>
            </tr>

            <tr>
              <td>
                {#if !selectedQuestion || selectedQuestion.category !== 'geography'}
                  <button on:click={() => selectRandomQuestion('geography', '200')} disabled={questions.geography.filter(q => q.points === '200' && !q.answered).length === 0}>
                    200
                  </button>
                {:else if selectedQuestion.points === '200'}
                  <QuestionDisplay category="geography" questionData={selectedQuestion} />
                {/if}
              </td>
              <td>
                {#if !selectedQuestion || selectedQuestion.category !== 'math'}
                  <button on:click={() => selectRandomQuestion('math', '200')} disabled={questions.math.filter(q => q.points === '200' && !q.answered).length === 0}>
                    200
                  </button>
                {:else if selectedQuestion.points === '200'}
                  <QuestionDisplay category="math" questionData={selectedQuestion} />
                {/if}
              </td>
              <td>
                {#if !selectedQuestion || selectedQuestion.category !== 'history'}
                  <button on:click={() => selectRandomQuestion('history', '200')} disabled={questions.history.filter(q => q.points === '200' && !q.answered).length === 0}>
                    200
                  </button>
                {:else if selectedQuestion.points === '200'}
                  <QuestionDisplay category="history" questionData={selectedQuestion} />
                {/if}
              </td>
            </tr>

            <tr>
              <td>
                {#if !selectedQuestion || selectedQuestion.category !== 'geography'}
                  <button on:click={() => selectRandomQuestion('geography', '300')} disabled={questions.geography.filter(q => q.points === '300' && !q.answered).length === 0}>
                    300
                  </button>
                {:else if selectedQuestion.points === '300'}
                  <QuestionDisplay category="geography" questionData={selectedQuestion} />
                {/if}
              </td>
              <td>
                {#if !selectedQuestion || selectedQuestion.category !== 'math'}
                  <button on:click={() => selectRandomQuestion('math', '300')} disabled={questions.math.filter(q => q.points === '300' && !q.answered).length === 0}>
                    300
                  </button>
                {:else if selectedQuestion.points === '300'}
                  <QuestionDisplay category="math" questionData={selectedQuestion} />
                {/if}
              </td>
              <td>
                {#if !selectedQuestion || selectedQuestion.category !== 'history'}
                  <button on:click={() => selectRandomQuestion('history', '300')} disabled={questions.history.filter(q => q.points === '300' && !q.answered).length === 0}>
                    300
                  </button>
                {:else if selectedQuestion.points === '300'}
                  <QuestionDisplay category="history" questionData={selectedQuestion} />
                {/if}
              </td>
            </tr>
          </tbody>
      </table>
  </div>
</div>

{#if selectedQuestion}
    <QuestionDisplay {selectedQuestion} />

    <div>
        <input bind:value={userInput} placeholder="Your answer" />
        <button on:click={handleSubmit}>Submit Answer</button>
    </div>
{/if}