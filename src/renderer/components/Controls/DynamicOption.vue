<template>
  <div class="form-group">
    <label for="name" class="font-weight-bold font-italic">
      {{ option.displayName }}
      <small class="text-muted" v-if="option.description">({{option.description}})</small>
    </label>
    <input v-if="option.type === 0" :placeholder="option.example" type="text" class="form-control" v-model="option.value" @change="(e) => onInputChange(e.target.value)"/>
    <textarea v-if="option.type === 1" :placeholder="option.example" rows="5" class="form-control" v-model="option.value" @change="(e) => onInputChange(e.target.value)"/>
    <input v-if="option.type === 2" :placeholder="option.example" type="password" class="form-control" v-model="option.value" @change="(e) => onInputChange(e.target.value)"/>
    <div class="form-check has-success" v-if="option.type === 3">
      <label class="form-check-label">
        <input type="checkbox" class="form-check-input" v-model="option.value" @change="(e) => onInputChange(e.target.value)">
      </label>
    </div>
    <div class="file-upload" v-if="option.type === 4">
      <span class="form-control">{{option.value}}</span>
      <input class="file" type="file" @change="onFileChange" :ref="`fileUpload${option.name}`"/>
    </div>
    <select v-if="option.type === 5" class="form-control" v-model="option.value">
      <option v-for="source in option.source" v-bind:key="source" v-bind:value="source" @click="() => onInputChange(source)">{{ source }}</option>
    </select>
  </div>
</template>

<script>
import { mapState } from 'vuex'
export default {
  name: 'dynamic-option',
  props: {
    option: Object
  },
  model: {
    event: 'change'
  },
  data() {
    return {
    }
  },
  created() {},
  async mounted() {
  },
  computed: {
    ...mapState({
    })
  },
  events: {},
  methods: {
    onFileChange(e) {
      this.$emit('change', e.target.files[0].path)
    },
    onInputChange(newValue) {
      this.$emit('change', newValue)
    }
  },
  watch: {
  },
  components: {
  }
}
</script>