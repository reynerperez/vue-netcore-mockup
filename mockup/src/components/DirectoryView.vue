<template>
    <v-treeview class="rounded-lg" :items="items" color="success" density="compact" item-title="title" item-value="id"
        open-on-click transition>
        <template v-slot:prepend="{ item }">
            <v-icon v-if="!item.children">mdi-file</v-icon>
            <v-icon v-else>mdi-folder</v-icon>
        </template>
        <template v-slot:append="{ item }">
            <v-menu>
                <template v-slot:activator="{ props }">
                    <v-btn icon="mdi-dots-vertical" density="compact" variant="plain" v-bind="props"></v-btn>
                </template>
                <v-list>
                    <v-list-item @click="handleAdd(item)">
                        <template v-slot:prepend>
                            <v-icon icon="mdi-plus"></v-icon>
                        </template>
                        <v-list-item-title>Add</v-list-item-title>
                    </v-list-item>
                    <v-list-item @click="handleEdit(item)">
                        <template v-slot:prepend>
                            <v-icon icon="mdi-pencil"></v-icon>
                        </template>
                        <v-list-item-title>Edit</v-list-item-title>
                    </v-list-item>
                    <v-list-item @click="handleDelete(item)">
                        <template v-slot:prepend>
                            <v-icon icon="mdi-delete"></v-icon>
                        </template>
                        <v-list-item-title>Delete</v-list-item-title>
                    </v-list-item>
                </v-list>
            </v-menu>

        </template>
    </v-treeview>
</template>

<script setup>
import { ref } from 'vue'
import { v4 as uuidv4 } from 'uuid';

const handleAdd = (item) => {
    let name = window.prompt("Node Name")
    if (!name) return
    if (Object.hasOwn(item, 'children'))
        item.children.push({ id: uuidv4(), title: name })
    else
        item.children = [{ id: uuidv4(), title: name }]
}
const handleEdit = (item) => {
    let name = window.prompt("Node Name", item.title)
    if (!name) return
    item.title = name
}
const handleDelete = (item) => {
    removeRecursive(items.value, item.id)
}
const removeRecursive = (items, id) => {
    items.forEach((element, index) => {
        const found = element.id === id
        if (found) {
            items.splice(index, 1);
        }
        else if (Object.hasOwn(element, 'children')) {
            removeRecursive(element.children, id)
        }
    });
}




function getNodeById(currentNode, id) {
    var node;
    if (currentNode.id === id) {
        return currentNode;
    }
    currentNode.children.some(child => node = getNodeById(child, id));
    return node;
}

const items = ref([
    {
        id: 1,
        title: 'Applications',
        children: [
            { id: 2, title: 'Calendar.app' },
            { id: 3, title: 'Chrome.app' },
            { id: 4, title: 'Webstorm.app' },
        ],
    },
    {
        id: 5,
        title: 'Documents',
        children: [
            {
                id: 6,
                title: 'vuetify',
                children: [
                    {
                        id: 7,
                        title: 'src',
                        children: [
                            { id: 8, title: 'index.ts' },
                            { id: 9, title: 'bootstrap.ts' },
                        ],
                    },
                ],
            },
            {
                id: 10,
                title: 'material2',
                children: [
                    {
                        id: 11,
                        title: 'src',
                        children: [
                            { id: 12, title: 'v-btn.ts' },
                            { id: 13, title: 'v-card.ts' },
                            { id: 14, title: 'v-window.ts' },
                        ],
                    },
                ],
            },
        ],
    },
    {
        id: 15,
        title: 'Downloads',
        children: [
            { id: 16, title: 'October.pdf' },
            { id: 17, title: 'November.pdf' },
            { id: 18, title: 'Tutorial.html' },
        ],
    },
    {
        id: 19,
        title: 'Videos',
        children: [
            {
                id: 20,
                title: 'Tutorials',
                children: [
                    { id: 21, title: 'Basic layouts.mp4' },
                    { id: 22, title: 'Advanced techniques.mp4' },
                    { id: 23, title: 'All about app.dir' },
                ],
            },
            { id: 24, title: 'Intro.mov' },
            { id: 25, title: 'Conference introduction.avi' },
        ],
    },
])


</script>