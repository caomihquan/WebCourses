
                                    const $=document.querySelector.bind(document);
                                    const $$=document.querySelectorAll.bind(document);
                                    
                                    const tabs=$$('.tab-item')
                                    
                                    const panes=$$('.tab-pane')
                                    const tabActive=$('.tab-item.active');
                                    const line=$('.tabs .line')
                                    const displaynone=$('.close-course')
                                    const lesson=$('.posts.p-4')
                                    const video=$('.w3l-courses.blog-posts.col-lg-8.pr-lg-0')
                                    const appearlesson=$('.appear-course')



                                    
                              
                                    line.style.left=tabActive.offsetLeft+'px';
                                    line.style.width=tabActive.offsetWidth +'px';
                                    
                                    tabs.forEach((tab,index) => {
                              
                                      const pane = panes[index]
                                      tab.onclick=function(){
                                        
                                        $('.tab-item.active').classList.remove('active')
                                        $('.tab-pane.active').classList.remove('active')
                              
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
                                      
                                      $('.btn-appear').style.display="flex"
                                      
                                    }
                                    appearlesson.onclick=()=>{
                                      lesson.classList.remove('display-none')
                                      video.classList.toggle('col-lg-8')
                                      video.classList.toggle('col-lg-12')
                                      appearlesson.style.display="none"
                                      
                                      $('.btn-appear').style.display="none"
                                    }
                                    
