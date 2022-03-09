
const tabs = document.querySelectorAll('.tab-item')                          
const panes = document.querySelectorAll('.tab-pane')
const tabActive = document.querySelector('.tab-item.active');
const line = document.querySelector('.tabs .line')
const displaynone = document.querySelector('.close-course')
const lesson = document.querySelector('.posts.p-4')
const video = document.querySelector('.w3l-courses.blog-posts.col-lg-8.pr-lg-0')
const appearlesson = document.querySelector('.appear-course')
line.style.left = tabActive.offsetLeft + 'px';
line.style.width = tabActive.offsetWidth + 'px';
                                    
                                    tabs.forEach((tab,index) => {
                              
                                      const pane = panes[index]
                                      tab.onclick=function(){
                                        
                                          document.querySelector('.tab-item.active').classList.remove('active')
                                          document.querySelector('.tab-pane.active').classList.remove('active')
                              
                                        line.style.left=this.offsetLeft+'px';
                                        line.style.width=this.offsetWidth +'px';
                              
                                        pane.classList.add('active')
                                        this.classList.add('active')
                                      }
                                    });

                                    displaynone.onclick=()=>{
                                      lesson.classList.add('display-none')
                                      video.classList.toggle('col-lg-8')
                                      video.classList.toggle('col-lg-12')
                                      appearlesson.style.display="flex"
                                      
                                        document.querySelector('.btn-appear').style.display="flex"
                                      
                                    }
                                    appearlesson.onclick=()=>{
                                      lesson.classList.remove('display-none')
                                      video.classList.toggle('col-lg-8')
                                      video.classList.toggle('col-lg-12')
                                      appearlesson.style.display="none"
                                      
                                        document.querySelector('.btn-appear').style.display="none"
                                    }

document.querySelector(".lesson_parent").addEventListener("click", function (event) {
    event.preventDefault()
});