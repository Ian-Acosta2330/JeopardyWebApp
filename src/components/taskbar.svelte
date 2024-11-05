<script>
    export const items = [
        {name: 'Home', href: '/'},
        {name: 'Game', href: '/game/'}
    ];
</script>

<style>
    .taskbar {
        display: flex;
        align-items: center;
        background-color: #333;
        color: white;
        padding: 15px;
    }
    .taskbar-item {
        margin-right: 20px;    
    }
    .taskbar-item a {
        color: white;
        text-decoration: none;
    }
    .taskbar-item a:hover {
        color: grey;
        text-decoration: underline;
        cursor: pointer;
    }

</style>

<div class="taskbar">
    {#each items as {name,href} }
    <div class="taskbar-item">
        <a href={href}>{name}</a>
    </div>
    {/each}
</div>


